using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Black.Beard.Policies.XUnit
{


    /// <summary>
    /// // génère la documentation
    /// </summary>
    public class JwtHelper
    {

        /// <summary>
        /// The private key used to sign the token
        /// </summary>
        public RsaSecurityKey PrivateKey { get; private set; }

        /// <summary>
        /// The public key used to validate the token
        /// </summary>
        public RsaSecurityKey PublicKey { get; private set; }

        /// <summary>
        /// The symmetric key used for signing and validation
        /// </summary>
        public SymmetricSecurityKey SymmetricKey { get; private set; }

        /// <summary>
        /// The issuer of the token
        /// </summary>
        public string Issuer { get; private set; }

        /// <summary>
        /// The audience of the token
        /// </summary>
        public string Audience { get; private set; }

        /// <summary>
        /// Create a new instance of JwtHelper
        /// </summary>
        public JwtHelper()
        {
            this._claims = new List<Claim>();
            this.Issuer = "NoneIssuer";
            this.Audience = "NoneAudience";
            _funcExpires = () => DateTime.UtcNow.AddHours(1);
        }

        /// <summary>
        /// Sets the issuer for the JWT token.
        /// </summary>
        /// <param name="issuer">The issuer to set. Cannot be null or empty.</param>
        /// <returns>The updated <see cref="JwtHelper"/> instance.</returns>
        public JwtHelper WithIssuer(string issuer)
        {
            this.Issuer = issuer;
            return this;
        }

        /// <summary>
        /// Sets the audience for the JWT token.
        /// </summary>
        /// <param name="audience">The audience to set. Cannot be null or empty.</param>
        /// <returns>The updated <see cref="JwtHelper"/> instance.</returns>
        public JwtHelper WithAudience(string audience)
        {
            this.Audience = audience;
            return this;
        }

        /// <summary>
        /// Sets the expiration time for the JWT token in hours.
        /// </summary>
        /// <param name="time">The number of hours until expiration. Must be greater than zero.</param>
        /// <returns>The updated <see cref="JwtHelper"/> instance.</returns>
        public JwtHelper WithExpirationInHour(int time)
        {
            _funcExpires = () => DateTime.UtcNow.AddHours(time);
            return this;
        }


        #region sign

        /// <summary>
        /// Initializes the helper with a symmetric key.
        /// </summary>
        /// <param name="key">The secret key used for symmetric signing. Must be at least 32 characters long.</param>
        /// <returns>The updated <see cref="JwtHelper"/> instance.</returns>
        public JwtHelper WithSymmetricKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key) || key.Length < 32)
                throw new ArgumentException("Key must be at least 32 characters long.", nameof(key));

            this.SymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return this;
        }

        /// <summary>
        /// Initializes the helper with an RSA key.
        /// </summary>
        /// <param name="keySize">The size of the RSA key. Default is 2048.</param>
        /// <param name="action">An optional action to configure the RSA instance.</param>
        /// <returns>The updated <see cref="JwtHelper"/> instance.</returns>
        public JwtHelper WithRsa(int keySize = 2048, Action<RSA> action = null)
        {
            _rsa = RSA.Create(keySize);
            this.PrivateKey = new RsaSecurityKey(_rsa);
            this.PublicKey = new RsaSecurityKey(_rsa.ExportParameters(false));

            if (action != null)
                action(_rsa);

            return this;
        }


        public JwtHelper WithRsa(Func<RSA> action)
        {
            if (action != null)
                _rsa = action();
            return this;
        }

        #endregion sign


        #region claims

        /// <summary>
        /// Adds a custom claim to the JWT token.
        /// </summary>
        /// <param name="type">The claim type. Cannot be null or empty.</param>
        /// <param name="value">The claim value. Cannot be null or empty.</param>
        /// <returns>The updated <see cref="JwtHelper"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown if type or value is null or empty.</exception>
        public JwtHelper WithClaim(string type, string value)
        {

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            this._claims.Add(new Claim(type, value));
            return this;
        }

        public JwtHelper WithClaimMail(string value)
        {

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            

            //System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames
            // JwtTypes
            // JwtClaimTypes 


            this._claims.Add(new Claim(JwtRegisteredClaimNames.Sub, value));
            return this;
        }

        public JwtHelper WithClaimJti()
        {
            this._claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            return this;
        }

        public JwtHelper WithScope(params string[] scopes)
        {
            foreach (var scope in scopes)
                if (!string.IsNullOrWhiteSpace(scope))
                    this._claims.Add(new Claim("scope", scope));
            return this;
        }

        public JwtHelper WithScope(Func<IEnumerable<string>> scopesFunc)
        {
            if (scopesFunc != null)
            {
                var scopes = scopesFunc();
                foreach (var scope in scopes)
                    if (!string.IsNullOrWhiteSpace(scope))
                        this._claims.Add(new Claim("scope", scope));
            }
            return this;
        }

        public JwtHelper WithRole(params string[] roles)
        {
            foreach (var role in roles)
                if (!string.IsNullOrWhiteSpace(role))
                    this._claims.Add(new Claim("role", role));
            return this;
        }

        public JwtHelper WithRole(Func<IEnumerable<string>> rolesFunc)
        {
            if (rolesFunc != null)
            {
                var roles = rolesFunc();
                foreach (var role in roles)
                    if (!string.IsNullOrWhiteSpace(role))
                        this._claims.Add(new Claim("role", role));
            }
            return this;
        }

        public JwtHelper WithClaims(params Claim[] claimsFunc)
        {
            foreach (var claim in claimsFunc)
                if (claim != null)
                    this._claims.Add(claim);
            return this;
        }

        public JwtHelper WithClaims(IEnumerable<Claim> claimsFunc)
        {

            foreach (var claim in claimsFunc)
                if (claim != null)
                    this._claims.Add(claim);
            return this;
        }

        public JwtHelper WithClaims(Func<IEnumerable<Claim>> claimsFunc)
        {

            if (claimsFunc != null)
            {
                var claims = claimsFunc();
                foreach (var claim in claims)
                    if (claim != null)
                        this._claims.Add(claim);
            }

            return this;

        }

        #endregion claims


        /// <summary>
        /// Generates a JWT token using the configured issuer, audience, claims, and signing credentials.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> representing the generated JWT token.
        /// </returns>
        /// <remarks>
        /// This method creates a JWT token with the specified claims, issuer, audience, and expiration time.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var jwtHelper = new JwtHelper()
        ///     .WithIssuer("yourIssuer")
        ///     .WithAudience("yourAudience")
        ///     .WithRsa(2048)
        ///     .WithClaim("type", "value");
        /// 
        /// string token = jwtHelper.Generate();
        /// </code>
        /// </example>
        public string Generate()
        {
            
            var credentials = SymmetricKey != null
                ? new SigningCredentials(SymmetricKey, SecurityAlgorithms.HmacSha256)
                : new SigningCredentials(PrivateKey, SecurityAlgorithms.RsaSha256);

            var token = new JwtSecurityToken(
                issuer: this.Issuer,
                audience: this.Audience,
                claims: this._claims.ToArray(),
                expires: _funcExpires(),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Validates a JWT token and returns the associated principal.
        /// </summary>
        /// <param name="token">The JWT token to validate. Cannot be null or empty.</param>
        /// <param name="validatedToken">The <see cref="JwtSecurityToken"/> that was validated.</param>
        /// <returns>
        /// An <see cref="IPrincipal"/> representing the validated token.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="token"/> is null or whitespace.</exception>
        /// <exception cref="ArgumentException"><paramref name="token"/>.Length is greater than <see cref="TokenHandler.MaximumTokenSizeInBytes"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the token is null or empty.</exception>
        /// <exception cref="SecurityTokenException">Thrown when the token is invalid.</exception>
        /// <exception cref="SecurityTokenExpiredException">Thrown when the token has expired.</exception>
        /// <exception cref="SecurityTokenInvalidIssuerException">Thrown when the token has an invalid issuer.</exception>
        /// <exception cref="SecurityTokenInvalidAudienceException">Thrown when the token has an invalid audience.</exception>
        /// <exception cref="SecurityTokenMalformedException"><paramref name="token"/> does not have 3 or 5 parts.</exception>
        /// <exception cref="SecurityTokenMalformedException"><see cref="CanReadToken(string)"/> returns false.</exception>
        /// <exception cref="SecurityTokenDecryptionFailedException"><paramref name="token"/> was a JWE was not able to be decrypted.</exception>
        /// <exception cref="SecurityTokenEncryptionKeyNotFoundException"><paramref name="token"/> 'kid' header claim is not null AND decryption fails.</exception>
        /// <exception cref="SecurityTokenException"><paramref name="token"/> 'enc' header claim is null or empty.</exception>
        /// <exception cref="SecurityTokenExpiredException"><paramref name="token"/> 'exp' claim is &lt; DateTime.UtcNow.</exception>
        /// <exception cref="SecurityTokenInvalidAudienceException"><see cref="TokenValidationParameters.ValidAudience"/> is null or whitespace and <see cref="TokenValidationParameters.ValidAudiences"/> is null. Audience is not validated if <see cref="TokenValidationParameters.ValidateAudience"/> is set to false.</exception>
        /// <exception cref="SecurityTokenInvalidAudienceException"><paramref name="token"/> 'aud' claim did not match either <see cref="TokenValidationParameters.ValidAudience"/> or one of <see cref="TokenValidationParameters.ValidAudiences"/>.</exception>
        /// <exception cref="SecurityTokenInvalidLifetimeException"><paramref name="token"/> 'nbf' claim is &gt; 'exp' claim.</exception>
        /// <exception cref="SecurityTokenInvalidSignatureException"><paramref name="token"/>.signature is not properly formatted.</exception> 
        /// <exception cref="SecurityTokenNoExpirationException"><paramref name="token"/> 'exp' claim is missing and <see cref="TokenValidationParameters.RequireExpirationTime"/> is true.</exception>
        /// <exception cref="SecurityTokenNoExpirationException"><see cref="TokenValidationParameters.TokenReplayCache"/> is not null and expirationTime.HasValue is false. When a TokenReplayCache is set, tokens require an expiration time.</exception>
        /// <exception cref="SecurityTokenNotYetValidException"><paramref name="token"/> 'nbf' claim is &gt; DateTime.UtcNow.</exception>
        /// <exception cref="SecurityTokenReplayAddFailedException"><paramref name="token"/> could not be added to the <see cref="TokenValidationParameters.TokenReplayCache"/>.</exception>
        /// <exception cref="SecurityTokenReplayDetectedException"><paramref name="token"/> is found in the cache.</exception>
        /// <remarks>
        /// This method validates the provided JWT token using the configured issuer, audience, and signing key.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var jwtHelper = new JwtHelper()
        ///     .WithIssuer("yourIssuer")
        ///     .WithAudience("yourAudience")
        ///     .WithRsa(2048);
        /// 
        /// var principal = jwtHelper.Validate("yourJwtToken");
        /// </code>
        /// </example>
        /// <returns> A <see cref="ClaimsPrincipal"/> from the JWT. Does not include claims found in the JWT header.</returns>
        public IPrincipal Validate(string token, out SecurityToken validatedToken)
        {

            if (token == null)
                throw new ArgumentNullException("token");

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = this.Issuer,
                ValidAudience = this.Audience,
                IssuerSigningKey = SymmetricKey ?? (SecurityKey)PublicKey
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);


            return principal;
        }

        private RSA _rsa;
        private readonly List<Claim> _claims;
        Func<DateTime> _funcExpires;

    }


}