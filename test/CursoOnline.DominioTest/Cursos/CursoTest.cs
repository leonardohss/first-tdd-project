using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

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
            const string nome = "Informática básica";
            const double cargaHoraria = 80;
            const string publicoAlvo = "Estudantes";
            const double valor = 950.00;

            //Action
            var curso = new Curso(nome, cargaHoraria, publicoAlvo, valor);

            //Assert
            Assert.Equal(nome, curso.Nome);
            Assert.Equal(cargaHoraria, curso.CargaHoraria);
            Assert.Equal(publicoAlvo, curso.PublicoAlvo);
            Assert.Equal(valor, curso.Valor);
        }
    }

    public class Curso
    {
        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public string PublicoAlvo { get; private set; }
        public double Valor { get; private set; }

        public Curso(string nome, double cargaHoraria, string publicoAlvo, double valor)
        {
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

    }
}
