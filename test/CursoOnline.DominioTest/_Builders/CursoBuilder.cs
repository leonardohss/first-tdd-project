﻿using CursoOnline.Dominio.Cursos;


namespace CursoOnline.DominioTest._Builders
{
    public class CursoBuilder
    {
        private string _nome = "Informática básica";
        private double _cargaHoraria = 80.00;
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
        private double _valor = 950.00;
        private string _descricao = "Uma descrição";

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }
        public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public Curso Build()
        {
            return new Curso(_nome, _cargaHoraria, _publicoAlvo, _valor, _descricao);
        }
    }
}
