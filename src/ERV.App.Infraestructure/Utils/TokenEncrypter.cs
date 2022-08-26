using ERV.App.Models.ViewModels.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using JsonProperty = Newtonsoft.Json.Serialization.JsonProperty;

namespace ERV.App.Infrastructure.Utils
{
    public static class TokenEncrypter
    {
        #region TokenRequest Input
        public static string Encrypt<T>(T value, string secretKey)
        {
            if (value == null || string.IsNullOrWhiteSpace(secretKey))
                return string.Empty;

            var text = JsonConvert.SerializeObject(value, new JsonSerializerSettings()
            {
                ContractResolver = ShouldSerializeTokenResolver.Instance,
                NullValueHandling = NullValueHandling.Ignore
            });

            var hmac = new HMACSHA1(Encoding.ASCII.GetBytes(secretKey));
            return Convert.ToBase64String(hmac.ComputeHash(Encoding.ASCII.GetBytes(text)));
        }
        #endregion
    }

    public class ShouldSerializeTokenResolver : DefaultContractResolver
    {
        public new static readonly ShouldSerializeTokenResolver Instance = new ShouldSerializeTokenResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property.DeclaringType == typeof(ClientInputDTO) && property.PropertyName == "Token")
                property.ShouldSerialize = instance => false;

            return property;
        }
    }
}
