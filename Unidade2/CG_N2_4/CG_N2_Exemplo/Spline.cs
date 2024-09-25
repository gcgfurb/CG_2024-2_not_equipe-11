using CG_Biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gcgcg
{
    public class Spline : Objeto
    {
        private List<Ponto4D> pontosControle = new List<Ponto4D>();

        public Spline(Objeto _paiRef, ref char _rotulo, List<Ponto4D> pontosSpline, int quantidadeSegmentos = 20) : base(_paiRef, ref _rotulo)
        {
            pontosControle = pontosSpline;

            foreach (var ponto in pontosSpline)
            {
                new Ponto(this, ref _rotulo, ponto);
            }

            Ponto4D pontoAnterior = CalcularPonto(0);
            for (int i = 0; i <= quantidadeSegmentos; i++)
            {
                Ponto4D pontoAtual = CalcularPonto(i / (double)quantidadeSegmentos);
                new SegReta(this, ref _rotulo, pontoAnterior, pontoAtual);
                pontoAnterior = pontoAtual;
            }
        }

        public Ponto4D CalcularPonto(double t)
        {
            return InterpolarRecursivo(pontosControle, t);
        }

        private Ponto4D InterpolarRecursivo(List<Ponto4D> pontos, double t)
        {
            if (pontos.Count == 1)
            {
                return pontos[0];
            }

            List<Ponto4D> pontosInterpolados = new List<Ponto4D>();
            for (int i = 0; i < pontos.Count - 1; i++)
            {
                pontosInterpolados.Add(Matematica.InterpolarRetaPonto(pontos[i], pontos[i + 1], t));
            }

            return InterpolarRecursivo(pontosInterpolados, t);
        }
    }
}
