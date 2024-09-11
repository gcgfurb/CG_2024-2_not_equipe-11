using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
    internal class Circulo : Objeto
    {
        public Circulo(Objeto _paiRef, ref char _rotulo) : this(_paiRef, ref _rotulo, new Ponto4D(0.0,0.0), 0.5, 72)
        {

        }

        public Circulo(Objeto _paiRef, ref char _rotulo, Ponto4D ptoCentro, double raio, int numPontos) : base(_paiRef, ref _rotulo)
        {
            PrimitivaTipo = PrimitiveType.Points;
            PrimitivaTamanho = 5;

            double anguloPontos = 360 / numPontos;

            for (int i = 0; i < numPontos; i++)
            {
                base.PontosAdicionar(Matematica.GerarPtosCirculo(anguloPontos*i, raio));
            }
            Atualizar();
        }

        private void Atualizar()
        {

            base.ObjetoAtualizar();
        }
    }
}
