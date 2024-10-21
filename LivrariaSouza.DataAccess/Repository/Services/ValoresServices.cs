using LivrariaSouza.Models.Models;
using System.Globalization;

namespace LivrariaSouza.DataAccess.Repository.Services
{
    public class ValoresServices
    {
        public object ValidarCaracteres(string valorString)
        {
            if (string.IsNullOrWhiteSpace(valorString))
                return "O valor não pode ser vazio.";

            if (!decimal.TryParse(valorString.Replace(".", ","), NumberStyles.Any, new CultureInfo("pt-BR"), out decimal valorConvertido))
            {
                return "O valor deve conter somente números e pode incluir uma vírgula ou ponto para os centavos.";
            }
            if (valorConvertido < 0)
            {
                return "O valor não pode ser negativo.";
            }

            return valorConvertido;
        }
    }
}
