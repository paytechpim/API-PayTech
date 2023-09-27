using Newtonsoft.Json;

namespace Paytech.Models
{
    public class EnderecoDTO
    {
        public int Id { get; set; }


        [JsonProperty("pais")]
        public string? Pais { get; set; }


        [JsonProperty("cep")]
        public string Cep { get; set; }


        [JsonProperty("bairro")]
        public string Bairro { get; set; }


        [JsonProperty("localidade")]
        public string Cidade { get; set; }


        [JsonProperty("uf")]
        public string Uf { get; set; }


        [JsonProperty("logradouro")]
        public string Rua { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

    }
}
