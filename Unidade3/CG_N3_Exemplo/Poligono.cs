using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace gcgcg
{
    internal class Poligono : Objeto
    {
        public Poligono(Objeto _paiRef, ref char _rotulo, List<Ponto4D> pontosPoligono) : base(_paiRef, ref _rotulo)
        {
            PrimitivaTipo = PrimitiveType.LineLoop;
            PrimitivaTamanho = 1;
            base.pontosLista = pontosPoligono;
            Atualizar();
        }

        private void Atualizar()
        {

            base.ObjetoAtualizar();
        }

        public int IndicePontoMaisProximo(Ponto4D ponto)
        {
            int indexMaisProximo = 0;
            double distanciaMaisProxima = Matematica.DistanciaQuadrado(ponto, this.pontosLista[0]);
            for (int i = 1; i < this.pontosLista.Count; i++)
            {
                double distancia = Matematica.DistanciaQuadrado(ponto, this.pontosLista[i]);
                if (distancia < distanciaMaisProxima)
                {
                    indexMaisProximo = i;
                    distanciaMaisProxima = distancia;
                }
            }
            return indexMaisProximo;
        }

        public void ApagarPontoMaisProximo(Ponto4D ponto)
        {
            int indiceMaisProximo = IndicePontoMaisProximo(ponto);
            this.pontosLista.RemoveAt(indiceMaisProximo);
            Atualizar();
        }

#if CG_Debug
        public override string ToString()
        {
            System.Console.WriteLine("__________________________________ \n");
            string retorno;
            retorno = "__ Objeto Poligono _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
            retorno += base.ImprimeToString();
            return retorno;
        }
#endif

    }
}
