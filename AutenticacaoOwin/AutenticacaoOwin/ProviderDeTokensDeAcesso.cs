using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AutenticacaoOwin.Models;

namespace AutenticacaoOwin
{
    public class ProviderDeTokensDeAcesso: OAuthAuthorizationServerProvider
    {
        //Responsável por fazer validações extras quando o usuário se autentica com token de acesso.
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        //Responsável por fornecer tokens de acesso com base em usuário e senha
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Encontrando usuario
            var usuario = VerificarLogin.verificarLogin(context.UserName, context.Password);
            //Cancelando emissão de token se o usuario não foi encontrado
            if(usuario == false)
            {
                context.SetError("invalid_grant", "Usuario não encontrado ou senha incorreta");
                return;
            }

            //Emitindo o token com informações extras
            //Se o usuário existe
            var identidadeUsuario = new ClaimsIdentity(context.Options.AuthenticationType);
            context.Validated(identidadeUsuario);
        }
    }
}