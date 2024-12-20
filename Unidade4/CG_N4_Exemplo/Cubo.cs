//https://github.com/mono/opentk/blob/main/Source/Examples/Shapes/Old/Cube.cs

#define CG_Debug
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Drawing;

namespace gcgcg
{
    internal class Cubo : Objeto
    {
        Ponto4D[] vertices;
        // int[] indices;
        Vector3[] normals = new Vector3[]
        {
            new Vector3(0,0,1f),
            new Vector3(0,1f,0),
            new Vector3(0,0,-1f),
            new Vector3(-1f,0,0),
            new Vector3(0,-1f,0),
            new Vector3(1f,0,0)
        };
        // int[] colors;

        public Cubo(Objeto _paiRef, ref char _rotulo, bool textura = false) : base(_paiRef, ref _rotulo)
        {
            PrimitivaTipo = PrimitiveType.TriangleFan;
            PrimitivaTamanho = 10;

            vertices = new Ponto4D[]
            {
                new Ponto4D(-1.0f, -1.0f,  1.0f, 1.0f, 0.0f, 0.0f),
                new Ponto4D( 1.0f, -1.0f,  1.0f, 1.0f, 1.0f, 0.0f),
                new Ponto4D( 1.0f,  1.0f,  1.0f, 1.0f, 1.0f, 1.0f),
                new Ponto4D(-1.0f,  1.0f,  1.0f, 1.0f, 0.0f, 1.0f),
                new Ponto4D(-1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f),
                new Ponto4D( 1.0f, -1.0f, -1.0f, 1.0f, 1.0f, 0.0f),
                new Ponto4D( 1.0f,  1.0f, -1.0f, 1.0f, 1.0f, 1.0f),
                new Ponto4D(-1.0f,  1.0f, -1.0f, 1.0f, 0.0f, 1.0f)
               
                /*
                 * 
                 *  // Frente (Z positivo)
            new Ponto4D(-1.0f, -1.0f,  1.0f, 1.0f, 0.0f, 0.0f), // Inferior Esquerdo
            new Ponto4D( 1.0f, -1.0f,  1.0f, 1.0f, 1.0f, 0.0f), // Inferior Direito
            new Ponto4D( 1.0f,  1.0f,  1.0f, 1.0f, 1.0f, 1.0f), // Superior Direito
            new Ponto4D(-1.0f,  1.0f,  1.0f, 1.0f, 0.0f, 1.0f), // Superior Esquerdo

            // Traseira (Z negativo)
            new Ponto4D(-1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f), // Inferior Esquerdo
            new Ponto4D( 1.0f, -1.0f, -1.0f, 1.0f, 1.0f, 0.0f), // Inferior Direito
            new Ponto4D( 1.0f,  1.0f, -1.0f, 1.0f, 1.0f, 1.0f), // Superior Direito
            new Ponto4D(-1.0f,  1.0f, -1.0f, 1.0f, 0.0f, 1.0f), // Superior Esquerdo
                
                */
            };

            // Frente (Z positivo)
            base.FilhoAdicionar(new Poligono(this, ref _rotulo, [new Ponto4D(-1.0f, -1.0f, 1.0f, 1.0f, 0.0f, 0.0f),
                                                                new Ponto4D(1.0f, -1.0f, 1.0f, 1.0f, 1.0f, 0.0f),
                                                                new Ponto4D(1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f),
                                                                new Ponto4D(-1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 1.0f)])
                                                                {PrimitivaTamanho = 10, 
                                                                PrimitivaTipo = PrimitiveType.TriangleFan,
                                                                shaderCor = textura? new Shader("Shaders/shaderTextura.vert", "Shaders/shaderTextura.frag") : new Shader("Shaders/shader.vert", "Shaders/shaderAmarela.frag")
            });
            /*
            base.PontosAdicionar(new Ponto4D(-1.0f, -1.0f, 1.0f, 1.0f, 0.0f, 0.0f));
            base.PontosAdicionar(new Ponto4D(1.0f, -1.0f, 1.0f, 1.0f, 1.0f, 0.0f));
            base.PontosAdicionar(new Ponto4D(1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f));
            base.PontosAdicionar(new Ponto4D(-1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 1.0f));
            */

            // Esquerda (X negativo)
            base.FilhoAdicionar(new Poligono(this, ref _rotulo, [new Ponto4D(-1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f),
                                                                new Ponto4D(-1.0f, 1.0f, -1.0f, 1.0f, 1.0f, 0.0f),
                                                                new Ponto4D(-1.0f, -1.0f, -1.0f, 1.0f, 1.0f, 1.0f),
                                                                new Ponto4D(-1.0f, -1.0f, 1.0f, 1.0f, 0.0f, 1.0f)])
            {
                PrimitivaTamanho = 10,
                PrimitivaTipo = PrimitiveType.TriangleFan,
                shaderCor = textura ? new Shader("Shaders/shaderTextura.vert", "Shaders/shaderTextura.frag") : new Shader("Shaders/shader.vert", "Shaders/shaderAmarela.frag")
            });
            /*
            base.PontosAdicionar(new Ponto4D(-1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f));
            base.PontosAdicionar(new Ponto4D(-1.0f, 1.0f, -1.0f, 1.0f, 1.0f, 0.0f));
            base.PontosAdicionar(new Ponto4D(-1.0f, -1.0f, -1.0f, 1.0f, 1.0f, 1.0f));
            base.PontosAdicionar(new Ponto4D(-1.0f, -1.0f, 1.0f, 1.0f, 0.0f, 1.0f));
            */

            // Inferior (Y negativo)
            base.FilhoAdicionar(new Poligono(this, ref _rotulo, [new Ponto4D(-1.0f, -1.0f, 1.0f, 1.0f, 0.0f, 0.0f),
                                                                new Ponto4D(-1.0f, -1.0f, -1.0f, 1.0f, 1.0f, 0.0f),
                                                                new Ponto4D(1.0f, -1.0f, -1.0f, 1.0f, 1.0f, 1.0f),
                                                                new Ponto4D(1.0f, -1.0f, 1.0f, 1.0f, 0.0f, 1.0f)])
            {
                PrimitivaTamanho = 10,
                PrimitivaTipo = PrimitiveType.TriangleFan,
                shaderCor = textura ? new Shader("Shaders/shaderTextura.vert", "Shaders/shaderTextura.frag") : new Shader("Shaders/shader.vert", "Shaders/shaderAmarela.frag")
            });

            //base.PontosAdicionar(new Ponto4D(-1.0f, -1.0f, 1.0f, 1.0f, 0.0f, 1.0f));
            //base.PontosAdicionar(new Ponto4D(-1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f));
            //base.PontosAdicionar(new Ponto4D(1.0f, -1.0f, -1.0f, 1.0f, 1.0f, 0.0f));
            //base.PontosAdicionar(new Ponto4D(1.0f, -1.0f, 1.0f, 1.0f, 1.0f, 1.0f));

            // Direita (X positivo)
            base.FilhoAdicionar(new Poligono(this, ref _rotulo, [new Ponto4D(1.0f, -1.0f, 1.0f, 1.0f, 0.0f, 0.0f),
                                                                new Ponto4D(1.0f, -1.0f, -1.0f, 1.0f, 1.0f, 0.0f),
                                                                new Ponto4D(1.0f, 1.0f, -1.0f, 1.0f, 1.0f, 1.0f),
                                                                new Ponto4D(1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 1.0f)])
            {
                PrimitivaTamanho = 10,
                PrimitivaTipo = PrimitiveType.TriangleFan,
                shaderCor = textura ? new Shader("Shaders/shaderTextura.vert", "Shaders/shaderTextura.frag") : new Shader("Shaders/shader.vert", "Shaders/shaderAmarela.frag")
            });

            //base.PontosAdicionar(new Ponto4D(1.0f, -1.0f, 1.0f, 1.0f, 1.0f, 0.0f));
            //base.PontosAdicionar(new Ponto4D(1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f));
            //base.PontosAdicionar(new Ponto4D(1.0f, 1.0f, -1.0f, 1.0f, 0.0f, 1.0f));
            //base.PontosAdicionar(new Ponto4D(1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f));

            // Superior (Y positivo)
            base.FilhoAdicionar(new Poligono(this, ref _rotulo, [new Ponto4D(1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f),
                                                                new Ponto4D(1.0f, 1.0f, -1.0f, 1.0f, 1.0f, 0.0f),
                                                                new Ponto4D(-1.0f, 1.0f, -1.0f, 1.0f, 1.0f, 1.0f),
                                                                new Ponto4D(-1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 1.0f)])
            {
                PrimitivaTamanho = 10,
                PrimitivaTipo = PrimitiveType.TriangleFan,
                shaderCor = textura ? new Shader("Shaders/shaderTextura.vert", "Shaders/shaderTextura.frag") : new Shader("Shaders/shader.vert", "Shaders/shaderAmarela.frag")
            });


            //base.PontosAdicionar(new Ponto4D(1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f));
            //base.PontosAdicionar(new Ponto4D(1.0f, 1.0f, -1.0f, 1.0f, 0.0f, 1.0f));
            //base.PontosAdicionar(new Ponto4D(-1.0f, 1.0f, -1.0f, 1.0f, 0.0f, 0.0f));
            //base.PontosAdicionar(new Ponto4D(-1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f));

            // Traseira (Z negativo)
            base.FilhoAdicionar(new Poligono(this, ref _rotulo, [new Ponto4D(-1.0f, 1.0f, -1.0f, 1.0f, 0.0f, 0.0f),
                                                                new Ponto4D(1.0f, 1.0f, -1.0f, 1.0f, 1.0f, 0.0f),
                                                                new Ponto4D(1.0f, -1.0f, -1.0f, 1.0f, 1.0f, 1.0f),
                                                                new Ponto4D(-1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 1.0f)])
            {
                PrimitivaTamanho = 10,
                PrimitivaTipo = PrimitiveType.TriangleFan,
                shaderCor = textura ? new Shader("Shaders/shaderTextura.vert", "Shaders/shaderTextura.frag") : new Shader("Shaders/shader.vert", "Shaders/shaderAmarela.frag")
            });

            //base.PontosAdicionar(new Ponto4D(-1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f));
            //base.PontosAdicionar(new Ponto4D(1.0f, -1.0f, -1.0f, 1.0f, 1.0f, 0.0f));
            //base.PontosAdicionar(new Ponto4D(1.0f, 1.0f, -1.0f, 1.0f, 1.0f, 1.0f));
            //base.PontosAdicionar(new Ponto4D(-1.0f, 1.0f, -1.0f, 1.0f, 0.0f, 1.0f));


            /*
            // // 0, 1, 2, 3 Face da frente
            base.PontosAdicionar(vertices[0]);
            base.PontosAdicionar(vertices[1]);
            base.PontosAdicionar(vertices[2]);
            base.PontosAdicionar(vertices[3]);

            // // 3, 2, 6, 7 Face de cima
            // 3, 2, 6, 7
            base.PontosAdicionar(vertices[3]);
            base.PontosAdicionar(vertices[2]);
            base.PontosAdicionar(vertices[6]);
            base.PontosAdicionar(vertices[7]);

            // // 4, 7, 6, 5 Face do fundo
            //5, 4, 7, 6
            base.PontosAdicionar(vertices[5]);
            base.PontosAdicionar(vertices[4]);
            base.PontosAdicionar(vertices[7]);
            base.PontosAdicionar(vertices[6]);

            // // 0, 3, 7, 4 Face esquerda
            base.PontosAdicionar(vertices[0]);
            base.PontosAdicionar(vertices[3]);
            base.PontosAdicionar(vertices[7]);
            base.PontosAdicionar(vertices[4]);

            // // 0, 4, 5, 1 Face de baixo
            base.PontosAdicionar(vertices[0]);
            base.PontosAdicionar(vertices[4]);
            base.PontosAdicionar(vertices[5]);
            base.PontosAdicionar(vertices[1]);

            // // 1, 5, 6, 2 Face direita
            //1, 5, 6, 2
            base.PontosAdicionar(vertices[1]);
            base.PontosAdicionar(vertices[5]);
            base.PontosAdicionar(vertices[6]);
            base.PontosAdicionar(vertices[2]);
            */

            Atualizar();
        }

        private void Atualizar()
        {

            base.ObjetoAtualizar();
        }

#if CG_Debug
        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Cubo _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
            retorno += base.ImprimeToString();
            return (retorno);
        }
#endif

    }
}
