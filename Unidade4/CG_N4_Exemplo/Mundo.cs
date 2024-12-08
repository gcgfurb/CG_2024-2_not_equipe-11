#define CG_DEBUG
#define CG_Gizmo      
#define CG_OpenGL      
// #define CG_OpenTK
// #define CG_DirectX      
// #define CG_Privado      

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System;
using OpenTK.Mathematics;
using System.Collections.Generic;

//FIXME: padrão Singleton

namespace gcgcg
{
    public class Mundo : GameWindow
    {
        private static Objeto mundo = null;
        private char rotuloNovo = '?';
        private Objeto objetoSelecionado = null;

        private readonly float[] _sruEixos =
        {
      -0.5f,  0.0f,  0.0f, /* X- */      0.5f,  0.0f,  0.0f, /* X+ */
       0.0f, -0.5f,  0.0f, /* Y- */      0.0f,  0.5f,  0.0f, /* Y+ */
       0.0f,  0.0f, -0.5f, /* Z- */      0.0f,  0.0f,  0.5f  /* Z+ */
    };

        private int _vertexBufferObject_sruEixos;
        private int _vertexArrayObject_sruEixos;

        private Shader _shaderAmarela;
        private Shader _shaderTextura;

        private Camera _camera;
        private float _yaw = 90;
        private float _pitch = 0;
        private float _radius = 5;

        public Mundo(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
           : base(gameWindowSettings, nativeWindowSettings)
        {
            mundo ??= new Objeto(null, ref rotuloNovo); //padrão Singleton
        }


        protected override void OnLoad()
        {
            base.OnLoad();

            Utilitario.Diretivas();
#if CG_DEBUG
            Console.WriteLine("Tamanho interno da janela de desenho: " + ClientSize.X + "x" + ClientSize.Y);
#endif

            GL.ClearColor(0.6f, 0.6f, 0.6f, 1.0f);

            GL.Enable(EnableCap.DepthTest);       // Ativar teste de profundidade
            GL.Enable(EnableCap.CullFace);     // Desenha os dois lados da face
                                               // GL.FrontFace(FrontFaceDirection.Cw);
                                               // GL.CullFace(CullFaceMode.FrontAndBack);


            _shaderAmarela = new Shader("Shaders/shader.vert", "Shaders/shaderAmarela.frag");

            Texture texture = Texture.LoadFromFile("Imagens/unnamed.jpg");
            texture.Use(TextureUnit.Texture0);

            _shaderTextura = new Shader("Shaders/shaderTextura.vert", "Shaders/shaderTextura.frag");

            #region Eixos: SRU  
            _vertexBufferObject_sruEixos = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_sruEixos);
            GL.BufferData(BufferTarget.ArrayBuffer, _sruEixos.Length * sizeof(float), _sruEixos, BufferUsageHint.StaticDraw);
            _vertexArrayObject_sruEixos = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject_sruEixos);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            #endregion

            #region Objeto: Cubo
            objetoSelecionado = new Cubo(mundo, ref rotuloNovo);
            objetoSelecionado.shaderCor = _shaderTextura;
            #endregion

            #region Objeto: ponto  
            objetoSelecionado = new Cubo(mundo, ref rotuloNovo);
            objetoSelecionado.MatrizEscalaXYZBBox(0.2,0.2,0.2);
            objetoSelecionado.MatrizTranslacaoXYZ(3,0,0);
            #endregion

            // objetoSelecionado.MatrizEscalaXYZ(0.2, 0.2, 0.2);

            objetoSelecionado.shaderCor = _shaderAmarela;

            _camera = new Camera(Vector3.UnitZ * 5, ClientSize.X / (float)ClientSize.Y);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            mundo.Desenhar(new Transformacao4D(), _camera);

#if CG_Gizmo
            Gizmo_Sru3D();
#endif
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            objetoSelecionado.MatrizRotacao(0.1);

            // ☞ 396c2670-8ce0-4aff-86da-0f58cd8dcfdc   TODO: forma otimizada para teclado.
            #region Teclado
            var estadoTeclado = KeyboardState;
            if (estadoTeclado.IsKeyDown(Keys.Escape))
                Close();
            #endregion

            #region  Mouse

            const float cameraSpeed = 1.0f;

            if (MouseState.IsButtonDown(MouseButton.Button1) && MouseState.Delta.X != 0)
            {
                _yaw += cameraSpeed * MouseState.Delta.X;
            }
            if (MouseState.IsButtonDown(MouseButton.Button1) && MouseState.Delta.Y != 0)
            {
                _pitch += cameraSpeed * MouseState.Delta.Y;
            }
            if (MouseState.ScrollDelta.Y != 0)
            {
                _radius += MouseState.ScrollDelta.Y * -0.1f;
            }

            // Atualiza a posição e orientação da câmera
            _camera.Orbit(new Vector3(0, 0, 0), _radius, _yaw, _pitch);

            #endregion

        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

#if CG_DEBUG
            Console.WriteLine("Tamanho interno da janela de desenho: " + ClientSize.X + "x" + ClientSize.Y);
#endif
            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
        }

        protected override void OnUnload()
        {
            mundo.OnUnload();

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteBuffer(_vertexBufferObject_sruEixos);
            GL.DeleteVertexArray(_vertexArrayObject_sruEixos);

            GL.DeleteProgram(_shaderAmarela.Handle);

            base.OnUnload();
        }

#if CG_Gizmo
        private void Gizmo_Sru3D()
        {
#if CG_OpenGL && !CG_DirectX
            var model = Matrix4.Identity;
            
#elif CG_DirectX && !CG_OpenGL
      Console.WriteLine(" .. Coloque aqui o seu código em DirectX");
#elif (CG_DirectX && CG_OpenGL) || (!CG_DirectX && !CG_OpenGL)
      Console.WriteLine(" .. ERRO de Render - escolha OpenGL ou DirectX !!");
#endif
        }
#endif

    }
}
