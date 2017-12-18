using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ECoinTrackerModels.Jwt
{
	public sealed class JwtTokenBuilder
	{
		private SecurityKey _securityKey;
		private string _subject = "";
		private string _issuer = "";
		private string _audience = "";
		private Dictionary<string, string> _claims = new Dictionary<string, string>();
		private int _expiryInMinutes = 5;

		public JwtTokenBuilder AddSecurityKey(SecurityKey securityKey)
		{
			_securityKey = securityKey;
			return this;
		}

		public JwtTokenBuilder AddSubject(string subject)
		{
			_subject = subject;
			return this;
		}

		public JwtTokenBuilder AddIssuer(string issuer)
		{
			_issuer = issuer;
			return this;
		}

		public JwtTokenBuilder AddAudience(string audience)
		{
			_audience = audience;
			return this;
		}

		public JwtTokenBuilder AddClaim(string type, string value)
		{
			_claims.Add(type, value);
			return this;
		}

		public JwtTokenBuilder AddClaims(Dictionary<string, string> claims)
		{
			_claims = new Dictionary<string, string>(_claims.Union(claims));
			return this;
		}

		public JwtTokenBuilder AddExpiry(int expiryInMinutes)
		{
			_expiryInMinutes = expiryInMinutes;
			return this;
		}

		public JwtToken Build()
		{
			EnsureArguments();

			var claims = new List<Claim>
			{
			  new Claim(JwtRegisteredClaimNames.Sub, _subject),
			  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			}
			.Union(_claims.Select(item => new Claim(item.Key, item.Value)));

			var token = new JwtSecurityToken(
							  _issuer,
							  _audience,
							  claims,
							  expires: DateTime.UtcNow.AddMinutes(_expiryInMinutes),
							  signingCredentials: new SigningCredentials(
														_securityKey,
														SecurityAlgorithms.HmacSha256));

			string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
			return new JwtToken
			{
				Value = tokenString,
				ValidTo = token.ValidTo
			};
		}

		#region " private "

		private void EnsureArguments()
		{
			if (_securityKey == null)
				throw new NullReferenceException("Security key");

			if (string.IsNullOrEmpty(_subject))
				throw new NullReferenceException("Subject");

			if (string.IsNullOrEmpty(_issuer))
				throw new NullReferenceException("Issuer");

			if (string.IsNullOrEmpty(_audience))
				throw new NullReferenceException("Audience");
		}

		#endregion
	}
}
