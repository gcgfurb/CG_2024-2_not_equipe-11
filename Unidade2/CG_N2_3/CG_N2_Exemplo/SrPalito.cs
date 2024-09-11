using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;

namespace gcgcg
{
    internal class SrPalito : Objeto
    {
        private double Raio { get; set; }
        private Ponto4D Centro { get; set; }
        private double Angulo { get; set; }

        public SrPalito(Objeto _paiRef, ref char _rotulo) : base(_paiRef, ref _rotulo)
        {
            PrimitivaTipo = PrimitiveType.Lines;
            PrimitivaTamanho = 1;
            Raio = 0.5;
            Centro = new Ponto4D(0.0,0.0);
            Angulo = 45;

            base.PontosAdicionar(Centro);
            base.PontosAdicionar(Matematica.GerarPtosCirculo(Angulo, Raio));

            Atualizar();
        }

        private void Atualizar()
        {

            base.ObjetoAtualizar();
        }

        public void AtualizarPe(double peInc)
        {
            for (int i = 0; i < 2; i++)
            {
                Ponto4D ponto = base.pontosLista[i];
                ponto.X += peInc;
            }
            Atualizar();
        }

        public void AtualizarRaio(double raioInc)
        {
            Raio += raioInc;
            Ponto4D novaCabeca = Matematica.GerarPtosCirculo(Angulo, Raio);
            novaCabeca.X += base.pontosLista[0].X;
            base.PontosAlterar(novaCabeca, 1);
            Atualizar();
        }

        public void AtualizarAngulo(double anguloInc)
        {
            Angulo += anguloInc;
            Ponto4D novaCabeca = Matematica.GerarPtosCirculo(Angulo, Raio);
            novaCabeca.X += base.pontosLista[0].X;
            base.PontosAlterar(novaCabeca, 1);
            Atualizar();
        }
    }
}
