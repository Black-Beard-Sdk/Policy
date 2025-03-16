using Bb.Policies;
using Bb.Policies.Asts;
using Microsoft.AspNetCore.Authorization;

namespace Bb
{


    public static class PolicyExtension
    {


        /// <summary>
        /// Append policies form specified file
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="filePath"></param>
        /// <param name="filter"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public static WebApplicationBuilder AddPolicy(WebApplicationBuilder builder, string filePath, Func<PolicyRule, bool>? filter = null, Action<AuthorizationOptions>? configureAction = null)
        {

            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            var services = builder.Services;

            PolicyContainer policies = Policy.ParsePath(filePath);
            if (!policies.Diagnostics.Success)
                throw new Exception("Failed to evaluate file policies");
            var e = new PolicyEvaluator(policies);

            services.AddAuthorization(options =>
            {

                foreach (var policyRule in policies.Rules)
                    if (filter != null && filter(policyRule))
                        options.AddPolicy(policyRule.Name, policy =>
                        {

                            policy.RequireAssertion(c =>
                            {
                                if (e.Evaluate(policyRule.Name, c.User, out RuntimeContext context))
                                    return true;
                                return false;
                            });

                        });

                if (configureAction != null)
                    configureAction(options);

            });

            return builder;

        }

    }


}
