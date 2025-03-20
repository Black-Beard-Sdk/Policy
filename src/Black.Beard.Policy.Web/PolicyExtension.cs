using Bb.Policies;
using Bb.Policies.Asts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Reflection;

namespace Bb
{


    public static class PolicyExtension
    {
        private static readonly Logger<WebApplication> _logger;

        static PolicyExtension()
        {

            _logger = new Logger<WebApplication>(new LoggerFactory());

        }

        /// <summary>
        /// Configures the policy evaluator service.
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        public static WebApplication ConfigurePolicy(this WebApplication web)
        {
            var evaluator = web.Services.GetRequiredService<PolicyEvaluator>();
            evaluator.ServiceProvider = web.Services.GetRequiredService<IServiceProvider>();
            return web;
        }

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
        public static WebApplicationBuilder AddPolicy(this WebApplicationBuilder builder, string filePath, Func<PolicyRule, bool>? filter = null, Action<AuthorizationOptions>? configureAction = null)
        {

            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            var services = builder.Services;

            PolicyContainer policies = Policy.ParsePath(filePath);
            if (!policies.Diagnostics.Success)
                throw new Exception("Failed to evaluate file policies");
            var evaluator = new PolicyEvaluator(policies);

            services.AddSingleton(evaluator);

            var items = GetAuthorizePoliciesFromAssemblies().ToList();

            services.AddAuthorization(options =>
            {
                ManageDefaults(options, policies, evaluator);

                foreach (var policyRule in policies.Rules)
                    if (filter != null && filter(policyRule))
                        options.AddPolicy(policyRule.Name, policy =>
                        {

                            if (items.Contains(policyRule.Name))
                                items.Remove(policyRule.Name);

                            policy.RequireAssertion(c => evaluator.Evaluate(policyRule.Name, c.User, out RuntimeContext context));

                        });

                if (configureAction != null)
                    configureAction(options);

                foreach (var item in items)
                    _logger.LogDebug($"Policy {item} not found in policies file");

            });

            return builder;

        }

        private static void ManageDefaults(AuthorizationOptions options, PolicyContainer policies, PolicyEvaluator evaluator)
        {
            if (policies.DefaultRule != null)
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAssertion((c) => evaluator.Evaluate("default", c.User, out RuntimeContext context))
                    .Build();
            }
            else
            {

            }

            if (policies.FallbackRule != null)
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAssertion((c) => evaluator.Evaluate("fallback", c.User, out RuntimeContext context))
                    .Build();
            }
            else if (policies.DefaultRule != null)
                options.FallbackPolicy = options.DefaultPolicy;

            else
            {

            }
        }

        /// <summary>
        /// Retrieves all policy names from AuthorizeAttribute across all loaded assemblies
        /// </summary>
        /// <returns>A list of unique policy names</returns>
        public static IEnumerable<string> GetAuthorizePoliciesFromAssemblies()
        {

            var policies = new HashSet<string>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    // Check assembly types
                    foreach (var type in assembly.GetTypes())
                    {
                        // Check type-level attributes
                        CollectPoliciesFromAttributes(type.GetCustomAttributes<AuthorizeAttribute>(), policies);

                        // Check method-level attributes
                        foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                            CollectPoliciesFromAttributes(method.GetCustomAttributes<AuthorizeAttribute>(), policies);

                        // Check property-level attributes
                        foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                            CollectPoliciesFromAttributes(property.GetCustomAttributes<AuthorizeAttribute>(), policies);

                    }
                }
                catch (ReflectionTypeLoadException)
                {
                    // Skip assemblies that cannot be loaded
                    continue;
                }
                catch (Exception)
                {
                    // Skip any other exceptions during reflection
                    continue;
                }
            }

            return policies;

        }

        private static void CollectPoliciesFromAttributes(IEnumerable<AuthorizeAttribute> attributes, HashSet<string> policies)
        {
            foreach (var attribute in attributes)
                if (!string.IsNullOrEmpty(attribute.Policy))
                    policies.Add(attribute.Policy);
        }


    }


}
