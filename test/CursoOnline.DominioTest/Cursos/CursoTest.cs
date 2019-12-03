using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ExpectedObjects;

namespace CursoOnline.DominioTest.Cursos
{
    public class CursoTest
    {
        /*
         "Eu, como administrador, quero criar e editar cursos para que sejam abertas matriculas para o mesmo."
         Critério de Aceite:
          - Criar um curso com nome, carga horária, publico alvo e valor do curso.
          - As opções para publico alvo são: Estudante, Universitário, Empregado e Empreendedor.
          - Todos os campos do curso são obrigatórios.
        */
        [Fact]
        public void DeveCriarCurso()
        {
            //Arrange
            /*Antes
            const string nome = "Informática básica";
            const double cargaHoraria = 80;
            const string publicoAlvo = "Estudantes";
            const double valor = 950.00;
            */
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950.00
            };//colocando o new apenas com a chave, cria-se um objeto anônimo

            //Action
            /*Antes
            var curso = new Curso(nome, cargaHoraria, publicoAlvo, valor);
            */
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            //Assert
            /*Antes
            Assert.Equal(nome, curso.Nome);
            Assert.Equal(cargaHoraria, curso.CargaHoraria);
            Assert.Equal(publicoAlvo, curso.PublicoAlvo);
            Assert.Equal(valor, curso.Valor);
            */
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);//necessário instalat a extensão ExpectedObjects e declarar o namespace
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950.00
            };

            var message = Assert.Throws<ArgumentException>(() => new Curso(nomeInvalido, cursoEsperado.CargaHoraria,
                cursoEsperado.PublicoAlvo, cursoEsperado.Valor)).Message;
            Assert.Equal("Nome inválido", message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950.00
            };

            var message = Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome, cargaHorariaInvalida,
                cursoEsperado.PublicoAlvo, cursoEsperado.Valor)).Message;

            Assert.Equal("Carga horária inválida", message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void NaoDeveCursoTerUmValorMenorQue1(double valorInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950.00
            };

            var message = Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria,
                cursoEsperado.PublicoAlvo, valorInvalido)).Message;

            Assert.Equal("Valor inválido", message);
        }

    }

    public enum PublicoAlvo
    {
        Estudante,
        Universitario,
        Empregado,
        Empreendedor
    }

    public class Curso
    {
        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }

        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("Nome inválido");
            }

            if (cargaHoraria < 1)
            {
                throw new ArgumentException("Carga horária inválida");
            }

            if (valor < 1)
            {
                throw new ArgumentException("Valor inválido");
            }

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

    }
}
