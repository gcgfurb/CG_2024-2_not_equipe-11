using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace gcgcg
{
    internal class Circulo : Objeto
    {
        private BBox BBoxInterna;
        private Ponto4D ptoCentro;
        private double raio;
        public Ponto4D centro;

        public Circulo(Objeto _paiRef, ref char _rotulo) : this(_paiRef, ref _rotulo, new Ponto4D(0.0,0.0), 0.5, 72)
        {

        }

        public Circulo(Objeto _paiRef, ref char _rotulo, Ponto4D ptoCentro, double raio, int numPontos) : base(_paiRef, ref _rotulo)
        {
            this.ptoCentro = ptoCentro;
            this.raio = raio;
            this.centro = ptoCentro;
            PrimitivaTipo = PrimitiveType.Points;
            PrimitivaTamanho = 2;

            double anguloPontos = 360 / numPontos;

            for (int i = 0; i < numPontos; i++)
            {
                base.PontosAdicionar(Matematica.GerarPtosCirculo(anguloPontos*i, raio));
            }
            BBoxInterna = new BBox();
            matrizGlobal = ObjetoMatrizGlobal(matriz);
            double ptosInternos = Math.Sqrt(2) / 2 * raio;
            BBoxInterna.Atualizar(matrizGlobal, new List<Ponto4D>() {new Ponto4D(ptosInternos, ptosInternos),
                                                                     new Ponto4D(ptosInternos, -ptosInternos),
                                                                     new Ponto4D(-ptosInternos, ptosInternos),
                                                                     new Ponto4D(-ptosInternos, -ptosInternos)});
            Atualizar();
        }

        private void Atualizar()
        {

            base.ObjetoAtualizar();
        }

        public bool Dentro(Ponto4D ponto)
        {
            if(BBoxInterna.Dentro(ponto))
                return true;
            if(Matematica.Distancia(new Ponto4D(), ponto) <= raio)
                return true;
            return false;
        }

        public void Mover(double x, double y)
        {
            for (int i = 0; i < this.pontosLista.Count; i++)
            {
                Ponto4D velhoPonto = this.pontosLista[i];
                Ponto4D novoPonto = new Ponto4D(velhoPonto.X + x, velhoPonto.Y + y);
                this.PontosAlterar(novoPonto, i);
            }
            centro.X += x;
            centro.Y += y;
            Atualizar();
        }
    }
}
