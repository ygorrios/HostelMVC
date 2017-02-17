using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Exceptions
{
    public enum Layer
    {
        Presentation,
        Business,
        Services,
        Data
    }

    public enum Errors
    {
        [StringDescription("Este registro não pôde ser excluído por ter associações com outro(s) registro(s)!")]
        ERRO_CHAVE_ESTRANGEIRA_EXCLUSAO = 1,
        [StringDescription("Este registro não pôde ser atualizado por não ter todas as associações necessárias!")]
        ERRO_CHAVE_ESTRANGEIRA_ATUALIZACAO = 2,
        [StringDescription("Este registro não pôde ser incluído por não ter todas as associações necessárias!")]
        ERRO_CHAVE_ESTRANGEIRA_INCLUSAO = 3,
        [StringDescription("A quantidade de caracteres informada excede o limite do campo na base de dados.")]
        ERRO_TAMANHO_CAMPO = 4,
        [StringDescription("Um campo obrigatórios não foi informado!")]
        ERRO_CAMPO_NOT_NULL = 5,
    }
}
