using System;
using Xunit;
using ExpectedObjects;
using CursoOnline.DominioTest._Util;
using Xunit.Abstractions;
using CursoOnline.DominioTest._Builders;
using Bogus;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.DominioTest.Cursos
{
    /*
        "Eu, como administrador, quero criar e editar cursos para que sejam abertas matriculas para o mesmo."
         Critério de Aceite:
            - Criar um curso com nome, carga horária, publico alvo e valor do curso.
            - As opções para publico alvo são: Estudante, Universitário, Empregado e Empreendedor.
            - Todos os campos do curso são obrigatórios.
            - Curso deve ter uma descrição.
    */

    public class CursoTest : IDisposable
    {
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;
        private readonly ITestOutputHelper _output;

        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor sendo executado");
            var faker = new Faker();

            _nome = faker.Random.Word();
            _cargaHoraria = faker.Random.Double(50, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Double(100, 1000);
            _descricao = faker.Lorem.Paragraph();
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado");
        }

        [Fact]
        public void DeveCriarCurso()
        {
            //Arrange - organização
            /*Antes
            const string nome = "Informática básica";
            const double cargaHoraria = 80;
            const string publicoAlvo = "Estudantes";
            const double valor = 950.00;
            */
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };//colocando o new apenas com a chave, cria-se um objeto anônimo

            //Action - ação
            /*Antes
            var curso = new Curso(nome, cargaHoraria, publicoAlvo, valor);
            */
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, 
                cursoEsperado.Valor, cursoEsperado.Descricao);

            //Assert - conferencia
            /*Antes
            Assert.Equal(nome, curso.Nome);
            Assert.Equal(cargaHoraria, curso.CargaHoraria);
            Assert.Equal(publicoAlvo, curso.PublicoAlvo);
            Assert.Equal(valor, curso.Valor);
            */
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);//necessário instalar a extensão ExpectedObjects e declarar o namespace
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {

            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo()
            .ComNome(nomeInvalido).Build()).ComMensagem("Nome inválido");

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {

            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo()
            .ComCargaHoraria(cargaHorariaInvalida).Build()).ComMensagem("Carga horária inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void NaoDeveCursoTerUmValorMenorQue1(double valorInvalido)
        {

            //Antes
            //var message = Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria,
            //    cursoEsperado.PublicoAlvo, valorInvalido)).Message;

            //Assert.Equal("Valor inválido", message);

            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo()
            .ComValor(valorInvalido).Build()).ComMensagem("Valor inválido");
        }
    }
}
