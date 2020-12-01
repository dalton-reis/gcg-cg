﻿#define CG_Gizmo
#define CG_Privado

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK.Input;
using CG_Biblioteca;

namespace gcgcg
{
  class Mundo : GameWindow
  {
    private static Mundo instanciaMundo = null;

    private Mundo(int width, int height) : base(width, height) { }

    public static Mundo GetInstance(int width, int height)
    {
      if (instanciaMundo == null)
        instanciaMundo = new Mundo(width, height);
      return instanciaMundo;
    }

    private CameraPerspective camera = new CameraPerspective();
    protected List<Objeto> objetosLista = new List<Objeto>();
    private ObjetoGeometria objetoSelecionado = null;
    private char objetoId = '@';
    private String menuSelecao = "";
    private char menuEixoSelecao = 'z';
    private short deslocamento = 0;

    private bool bBoxDesenhar = false;
    int mouseX, mouseY;   //TODO: achar método MouseDown para não ter variável Global
    private Poligono objetoNovo = null;

#if CG_Privado
    // private Retangulo obj_Retangulo;
    // private Privado_SegReta obj_SegReta;
    // private Privado_Circulo obj_Circulo;
    // private Cilindro obj_Cilindro;
    // private Esfera obj_Esfera;
    // private Cone obj_Cone;
#endif
    private Cubo obj_Cubo;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [  H     ] mostra teclas usadas. ");


#if CG_Privado
      // objetoId = Utilitario.charProximo(objetoId);
      // obj_Retangulo = new Retangulo(objetoId, null, new Ponto4D(50, 50, 0), new Ponto4D(150, 150, 0));
      // obj_Retangulo.ObjetoCor.CorR = 255; obj_Retangulo.ObjetoCor.CorG = 0; obj_Retangulo.ObjetoCor.CorB = 255;
      // objetosLista.Add(obj_Retangulo);
      // objetoSelecionado = obj_Retangulo;

      // objetoId = Utilitario.charProximo(objetoId);
      // obj_SegReta = new Privado_SegReta(objetoId, null, new Ponto4D(50, 150), new Ponto4D(150, 250));
      // obj_SegReta.ObjetoCor.CorR = 255; obj_SegReta.ObjetoCor.CorG = 99; obj_SegReta.ObjetoCor.CorB = 71;
      // objetosLista.Add(obj_SegReta);
      // objetoSelecionado = obj_SegReta;

      // objetoId = Utilitario.charProximo(objetoId);
      // obj_Circulo = new Privado_Circulo(objetoId, null, new Ponto4D(100, 300), 50);
      // obj_Circulo.ObjetoCor.CorR = 177; obj_Circulo.ObjetoCor.CorG = 166; obj_Circulo.ObjetoCor.CorB = 136;
      // objetosLista.Add(obj_Circulo);
      // objetoSelecionado = obj_Circulo;
      // obj_Cilindro = new Cilindro(objetoId, null);
      // obj_Cilindro.ObjetoCor.CorR = 177; obj_Cilindro.ObjetoCor.CorG = 166; obj_Cilindro.ObjetoCor.CorB = 136;
      // objetosLista.Add(obj_Cilindro);
      // obj_Cilindro.EscalaXYZ(50, 50, 50);
      // obj_Cilindro.TranslacaoXYZ(150, 0, 0);

      // objetoId = Utilitario.charProximo(objetoId);
      // obj_Esfera = new Esfera(objetoId, null);
      // obj_Esfera.ObjetoCor.CorR = 177; obj_Esfera.ObjetoCor.CorG = 166; obj_Esfera.ObjetoCor.CorB = 136;
      // objetosLista.Add(obj_Esfera);
      // obj_Esfera.EscalaXYZ(50, 50, 50);
      // obj_Esfera.TranslacaoXYZ(200, 0, 0);

      // objetoId = Utilitario.charProximo(objetoId);
      // obj_Cone = new Cone(objetoId, null);
      // obj_Cone.ObjetoCor.CorR = 177; obj_Cone.ObjetoCor.CorG = 166; obj_Cone.ObjetoCor.CorB = 136;
      // objetosLista.Add(obj_Cone);
      // obj_Cone.EscalaXYZ(50, 50, 50);
      // obj_Cone.TranslacaoXYZ(250,0,0);
#endif
      // Objeto Chão
      objetoId = Utilitario.charProximo(objetoId);
      obj_Cubo = new Cubo(objetoId, null);
      objetosLista.Add(obj_Cubo);
      objetoSelecionado = obj_Cubo;

      // obj_Cubo.EscalaXYZ(7, 1, 5);
      // obj_Cubo.TranslacaoXYZ(3,-1,2);

      // // Objeto Parede 1
      // objetoId = Utilitario.charProximo(objetoId);
      // obj_Cubo = new Cubo(objetoId, null);
      // objetosLista.Add(obj_Cubo);
      // obj_Cubo.TranslacaoXYZ(0,0,0);

      // // Objeto Parede 2
      // objetoId = Utilitario.charProximo(objetoId);
      // obj_Cubo = new Cubo(objetoId, null);
      // objetosLista.Add(obj_Cubo);
      // obj_Cubo.TranslacaoXYZ(0,0,1);

      // // Objeto Personagem
      // objetoId = Utilitario.charProximo(objetoId);
      // obj_Cubo = new Cubo(objetoId, null);
      // objetosLista.Add(obj_Cubo);
      // obj_Cubo.EscalaXYZ(0.7, 0.7, 0.7);
      // obj_Cubo.TranslacaoXYZ(1,0,3);

      // objetoSelecionado = obj_Cubo;

      // camera.Eye = new Vector3(3.5f, -1, 3);
      camera.Eye = new Vector3(5.0f, 5.0f, 5.0f);
      // camera.At = new Vector3(3.5f, -0.5f, 2.5f);
      // camera.Near = 0.1f;
      // camera.Far = 600.0f;

      GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
      GL.Enable(EnableCap.DepthTest);
      GL.Enable(EnableCap.CullFace);
    }
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);

      GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

      Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(camera.Fovy, Width / (float)Height, camera.Near, camera.Far);
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadMatrix(ref projection);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      Matrix4 modelview = Matrix4.LookAt(camera.Eye, camera.At, camera.Up);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadMatrix(ref modelview);
#if CG_Gizmo      
      Sru3D();
#endif
      for (var i = 0; i < objetosLista.Count; i++)
        objetosLista[i].Desenhar();
      if (bBoxDesenhar && (objetoSelecionado != null))
        objetoSelecionado.BBox.Desenhar();
      this.SwapBuffers();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      Console.Clear(); //TODO: não funciona.
      Console.WriteLine("__ "+menuSelecao);
      if (e.Key == Key.H) Utilitario.AjudaTeclado();
      else if (e.Key == Key.Escape) Exit();

      else if (e.Key == Key.X) menuEixoSelecao = 'x';
      else if (e.Key == Key.Y) menuEixoSelecao = 'y';
      else if (e.Key == Key.Z) menuEixoSelecao = 'z';
      else if (e.Key == Key.Minus) deslocamento--;
      else if (e.Key == Key.Plus) deslocamento++;
      else if (e.Key == Key.C) menuSelecao = "[menu] C: Câmera";
      else if (e.Key == Key.O) menuSelecao = "[menu] O: Objeto";

      // Menu: seleção
      else if (menuSelecao.Equals("[menu] C: Câmera")) camera.MenuTecla(e.Key, menuEixoSelecao, deslocamento);
      // else if (menuSelecao.Equals("[menu] O: Objeto")) //FIXME: terminar igual a camera
      // {
      //   if (objetoSelecionado != null) objetoSelecionado.MenuTecla(e.Key, menuEixoSelecao, deslocamento);
      // }

      else if (e.Key == Key.E)
      {
        Console.WriteLine("--- Objetos / Pontos: ");
        for (var i = 0; i < objetosLista.Count; i++)
        {
          Console.WriteLine(objetosLista[i]);
        }
      }
      else if (e.Key == Key.O)
        bBoxDesenhar = !bBoxDesenhar;
      else if (e.Key == Key.Enter)
      {
        if (objetoNovo != null)
        {
          objetoNovo.PontosRemoverUltimo();   // N3-Exe6: "truque" para deixar o rastro
          objetoSelecionado = objetoNovo;
          objetoNovo = null;
        }
      }
      else if (e.Key == Key.Space)
      {
        if (objetoNovo == null)
        {
          objetoId = Utilitario.charProximo(objetoId);
          objetoNovo = new Poligono(objetoId, null);
          objetosLista.Add(objetoNovo);
          objetoNovo.PontosAdicionar(new Ponto4D(mouseX, mouseY));
          objetoNovo.PontosAdicionar(new Ponto4D(mouseX, mouseY));  // N3-Exe6: "troque" para deixar o rastro
        }
        else
          objetoNovo.PontosAdicionar(new Ponto4D(mouseX, mouseY));
      }
      else if (objetoSelecionado != null)
      {
        if (e.Key == Key.M)
          Console.WriteLine(objetoSelecionado.Matriz);
        else if (e.Key == Key.P)
          Console.WriteLine(objetoSelecionado);
        else if (e.Key == Key.I)
          objetoSelecionado.AtribuirIdentidade();
        //TODO: não está atualizando a BBox com as transformações geométricas
        else if (e.Key == Key.Left)
          objetoSelecionado.TranslacaoXYZ(-1, 0, 0);
        else if (e.Key == Key.Right)
          objetoSelecionado.TranslacaoXYZ(1, 0, 0);
        else if (e.Key == Key.Up)
          objetoSelecionado.TranslacaoXYZ(0, 0, -1);
        else if (e.Key == Key.Down)
          objetoSelecionado.TranslacaoXYZ(0, 0, 1);
        else if (e.Key == Key.Number8)
          objetoSelecionado.TranslacaoXYZ(0, 1, 0);
        else if (e.Key == Key.Number9)
          objetoSelecionado.TranslacaoXYZ(0, -1, 0);
        else if (e.Key == Key.PageUp)
          objetoSelecionado.EscalaXYZ(2, 2, 2);
        else if (e.Key == Key.PageDown)
          objetoSelecionado.EscalaXYZ(0.5, 0.5, 0.5);
        else if (e.Key == Key.Home)
          objetoSelecionado.EscalaXYZBBox(0.5, 0.5, 0.5);
        else if (e.Key == Key.End)
          objetoSelecionado.EscalaXYZBBox(2, 2, 2);
        else if (e.Key == Key.Number1)
          objetoSelecionado.Rotacao(10, menuEixoSelecao);
        else if (e.Key == Key.Number2)
          objetoSelecionado.Rotacao(-10, menuEixoSelecao);
        else if (e.Key == Key.Number3)
          objetoSelecionado.RotacaoZBBox(10, menuEixoSelecao);
        else if (e.Key == Key.Number4)
          objetoSelecionado.RotacaoZBBox(-10, menuEixoSelecao);
        else if (e.Key == Key.Number9)
          objetoSelecionado = null;                     // desmacar objeto selecionado
        else
          Console.WriteLine(" __ Tecla não implementada.");
      }
      else
        Console.WriteLine(" __ Tecla não implementada.");
    }

    //TODO: não está considerando o NDC
    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
      mouseX = e.Position.X; mouseY = 600 - e.Position.Y; // Inverti eixo Y
      if (objetoNovo != null)
      {
        objetoNovo.PontosUltimo().X = mouseX;
        objetoNovo.PontosUltimo().Y = mouseY;
      }
    }

#if CG_Gizmo
    private void Sru3D()
    {
      GL.LineWidth(1);
      GL.Begin(PrimitiveType.Lines);
      // GL.Color3(1.0f,0.0f,0.0f);
      GL.Color3(Convert.ToByte(255), Convert.ToByte(0), Convert.ToByte(0));
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      // GL.Color3(0.0f,1.0f,0.0f);
      GL.Color3(Convert.ToByte(0), Convert.ToByte(255), Convert.ToByte(0));
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
      // GL.Color3(0.0f,0.0f,1.0f);
      GL.Color3(Convert.ToByte(0), Convert.ToByte(0), Convert.ToByte(255));
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
      GL.End();
    }
#endif    
  }
  class Program
  {
    static void Main(string[] args)
    {
      Mundo window = Mundo.GetInstance(600, 600);
      window.Title = "CG_N4";
      window.Run(1.0 / 60.0);
    }
  }
}
