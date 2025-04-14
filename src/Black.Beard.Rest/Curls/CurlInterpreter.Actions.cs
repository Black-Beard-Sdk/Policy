
using Bb.Http;
using RestSharp;
using System.Reflection;

namespace Bb.Curls
{

    // https://curlconverter.com/csharp/
    public partial class CurlInterpreter
    {


        internal static CurlInterpreterAction? SetUrl(ArgumentSource arguments)
        {

            var arg = arguments.GetArgument();

            Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
            {
                Url url = sender.FirstValue;
                //var req = context.Request = url.Request();
                context.Headers.Add("Host", $"{url.Host}:{url.Port}");
            };

            return new CurlInterpreterAction(action, arg) { Priority = 0 };

        }

        internal static CurlInterpreterAction? Request(ArgumentSource arguments)
        {

            Action<CurlInterpreterAction, CurlContext> action = null;

            if (arguments.ReadNext())
            {

                var argText = arguments.Current;
                switch (argText.ToUpper())
                {
                    case "POST":
                        action = (sender, context) => context.Request.Method = Method.Post;
                        break;

                    case "GET":
                        action = (sender, context) => context.Request.Method = Method.Get;
                        break;

                    case "HEAD":
                        action = (sender, context) => context.Request.Method = Method.Head;
                        break;

                    case "PUT":
                        action = (sender, context) => context.Request.Method = Method.Put;
                        break;

                    case "DELETE":
                        action = (sender, context) => context.Request.Method = Method.Delete;
                        break;                    

                    case "OPTIONS":
                        action = (sender, context) => context.Request.Method = Method.Options;
                        break;

                    //case "TRACE":
                    //    action = (sender, context) => context.Request.Method = Method.Trace;
                    //    break;
                    //case "CONNECT":
                    //    action = (sender, context) => context.Request.Method = new Method("CONNECT");
                    //    break;
                    //case "PATCH":
                    //    action = (sender, context) => context.Request.Method = new Method("PATCH");
                    //    break;
                }

                if (action != null)
                    return new CurlInterpreterAction(action) { Priority = 1 };

                arguments.Failed($"Failed to read action {argText}");

            }
            else
                arguments.Failed($"Failed to read action");

            return null;
        }

        internal static CurlInterpreterAction? Header(ArgumentSource arguments)
        {

            bool isMultipart = false;

            if (arguments.ReadNext())
            {

                var arg = arguments.Current.Split(':');
                var a = new Argument(arg[1].Trim(), arg[0].Trim());

                if (a.Name == "Content-Type" && a.Value.ToLower().Trim().StartsWith("multipart"))
                    isMultipart = true;

                static void action(CurlInterpreterAction sender, CurlContext context)
                {
                    var arg = sender.First;
                    if (!context.Headers.Contains(arg.Name))
                        context.Headers.AddOrReplace(arg.Name, arg.Value);
                    else
                    {
                        Stop();
                    }
                }

                var r = new CurlInterpreterAction(action, a) { Priority = 2 };

                if (isMultipart)
                    r.Add("isMultipart", "1");

                return r;

            }

            arguments.Failed($"Failed to read header");

            return null;

        }

        internal static CurlInterpreterAction? Form(ArgumentSource arguments)
        {

            if (arguments.ReadNext())
            {

                //var arg = arguments.Current.Split(';');
                //List<Argument> _args = new List<Argument>(arg.Length);

                //foreach (var item in arg)
                //{
                //    var i = item.Split('=');
                //    _args.Add(new Argument(i[1], i[0].Trim()));
                //}

                //Action<CurlInterpreterAction, CurlContext> actionForm = (sender, context) =>
                //{

                //    var args = sender.Arguments;
                //    var req = context.Request;

                //    MultipartFormDataContent form = null;

                //    bool created = false;
                //    if (req.Content == null)
                //    {
                //        req.Content = form = new MultipartFormDataContent();
                //        created = true;
                //    }

                //    else if (req.Content is MultipartFormDataContent)
                //        form = (MultipartFormDataContent)req.Content;

                //    else
                //    {
                //        Stop();
                //    }

                //    var type = sender.Arguments.Where(c => c.Name == "type").FirstOrDefault()?.Value;

                //    if (type == null)
                //        Stop();

                //    var list = sender.Arguments.Where(c => c.Name != "type").ToList();

                //    foreach (var item in list)
                //        if (item.Value.StartsWith("@"))
                //        {

                //            if (created)
                //                context.Request.Headers.Remove("Content-Type");

                //            form.AddFile(item.Name, item.Value, type);
                //        }
                //        else
                //        {



                //        }

                //};

                //return new CurlInterpreterAction(actionForm, _args.ToArray()) { Priority = 3 };

            }

            return null;

        }

        internal static CurlInterpreterAction? Data(ArgumentSource arguments)
        {

            if (arguments.ReadNext())
            {

                //Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
                // {

                //     if (!context.Headers.TryGetFirst("Content-Type", out var contentType))
                //         contentType = Bb.Http.ContentType.ApplicationJson;

                //     var datas = sender.Arguments.First().Value?.ToString() ?? string.Empty;
                //     context.Request.Content = new StringContent(datas, null, contentType);

                // };

                //return new CurlInterpreterAction(action, new Argument(arguments.Current))
                //{
                //    Priority = 60
                //};

            }

            return null;

        }

        internal static CurlInterpreterAction? Cookie(ArgumentSource arguments)
        {

            if (arguments.ReadNext())
            {

                var arg = arguments.Current.Split(':');
                var a = new Argument(arg[1].Trim(), arg[0].Trim());

                static void action(CurlInterpreterAction sender, CurlContext context)
                {

                    var arg = sender.First;
                    context.Request.AddCookie(arg.Name, arg.Value, "", "");
                }

                var r = new CurlInterpreterAction(action, a) { Priority = 2 };

                return r;

            }

            arguments.Failed($"Failed to read header");

            return null;


        }

        internal static CurlInterpreterAction? CookieJar(ArgumentSource arguments)
        {

            //if (arguments.ReadNext())
            //{

            //    var arg = arguments.Current.Split(':');
            //    var a = new Argument(arg[1].Trim(), arg[0].Trim());

            //    static void action(CurlInterpreterAction sender, CurlContext context)
            //    {

            //        CookieJar jar = null;
            //        if (context.Request.CookieJar == null)
            //            context.Request.CookieContainer.WithCookies(jar = new CookieJar());

            //        var arg = sender.First;

            //        jar.AddOrReplace(arg.Name, arg.Value, context.Request.Url.Root);

            //    }

            //    var r = new CurlInterpreterAction(action, a) { Priority = 2 };

            //    return r;

            //}

            //arguments.Failed($"Failed to read header");

            return null;

        }


        internal static CurlInterpreterAction? User(ArgumentSource arguments)
        {

            if (arguments.ReadNext())
            {

                string security = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(arguments.Current));
                var headerValue = $"Authorization: Basic {security}";
                var a = new Argument(headerValue);

                Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
                 {
                     string name = "Authorization";
                     context.Headers.AddOrReplace(name, sender.First.Value);
                 };

                return new CurlInterpreterAction(action, a) { Priority = 20 };

            }

            return null;

        }


        //internal static CurlInterpreterAction? Head(ArgumentSource arguments)
        //{
        //    Stop();

        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };

        //}

        //internal static CurlInterpreterAction? HaproxyProtocol(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? HappyEyeballsTimeoutMs(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();


        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Globoff(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Get(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? FtpSslControl(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? FtpSslCccMode(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? FtpSslCcc(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? FtpSkipPasvIp(ArgumentSource arguments)
        //{
        //    Stop();

        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? FtpPret(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? FtpPort(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? FtpPasv(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? FtpMethod(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? FtpCreateDirs(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? FtpAlternativeToUser(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? FtpAccount(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? FormString(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? FalseStart(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? FailEarly(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Fail(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Expect100Timeout(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Engine(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? EgdFile(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? DumpHeader(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? DohUrl(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? DnsServers(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? DnsIpv6Addr(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? DnsIpv4Addr(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? DnsInterface(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? DisallowUsernameInUrl(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? DisableEpsv(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? DisableEprt(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Disable(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Digest(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Delegation(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? DataUrlencode(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? DataRaw(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? DataBinary(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? DataAscii(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Crlfile(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Crlf(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? CreateDirs(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ContinueAt(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ConnectTo(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ConnectTimeout(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Config(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? CompressedSsh(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Compressed(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Ciphers(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? CertType(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? CertStatus(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Cert(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Capath(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? CaCert(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Basic(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? AppendMethod(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyNegotiate(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyKeyType(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyKey(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyInsecure(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyHeader(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyDigest(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyCrlfile(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyCiphers(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyCertType(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyCert(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyCapath(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyCacert(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyBasic(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyAnyauth(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Proxy(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProtoRedir(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProtoDefault(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Proto(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProgressBar(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Preproxy(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Post303(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Post302(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Post301(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Pinnedpubkey(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? PathAsIs(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Pass(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Output(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Oauth2Bearer(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? NtlmWb(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Ntlm(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Noproxy(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? NoSessionid(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? NoNpn(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? NoKeepalive(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? NoBuffer(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? NoAlpn(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Next(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? NetrcOptional(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? NetrcFile(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Netrc(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Negotiate(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Metalink(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? MaxTime(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? MaxRedirs(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? MaxFilesize(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Manual(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? MailRcpt(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? MailFrom(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? MailAuth(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? LoginOptions(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? LocationTrusted(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Location(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? LocalPort(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ListOnly(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? LimitRate(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Libcurl(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Krb(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? KeyType(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Key(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? KeepaliveTime(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? JunkSessionCookies(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Ipv6(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Ipv4(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Interface
        //{
        //    get
        //    {
        //        Stop();
        //        Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //         {
        //             Stop();
        //             return await sender.CallNext(context);
        //         };

        //        return new CurlInterpreterAction(action) { Priority = 20 };
        //    }
        //}


        //internal static CurlInterpreterAction? Insecure(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Include(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? IgnoreContentLength(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Http2PriorKnowledge(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Http2(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Http11(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Http10(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Http09(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Hostpubmd5(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Help(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? RemoteHeaderName(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Referer(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Raw(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Range(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? RandomFile(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Quote(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Pubkey(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Proxytunnel(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Proxy10(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyUser(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyTlsv1(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyTlsuser(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyTlspassword(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyTlsauthtype(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyTls13Ciphers(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxySslAllowBeast(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyServiceName(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyPinnedpubkey(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyPass(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ProxyNtlm(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}


        //internal static CurlInterpreterAction? Anyauth(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? AltSvc(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? AbstractUnixSocket(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Xattr(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? WriteOut(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Version(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Verbose(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? UserAgent(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? UseAscii(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Url(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? UploadFile(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? UnixSocket(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? TraceTime(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? TraceAscii(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Trace(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? TrEncoding(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Tlsv13(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Tlsv12(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Tlsv11(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Tlsv10(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Tlsv1(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Tlsuser(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Tlspassword(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Tlsauthtype(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Tls13Ciphers(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? TlsMax(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? TimeCond(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? TftpNoOptions(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? TftpBlksize(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? TelnetOption(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? TcpNodelay(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? TcpFastopen(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? SuppressConnectHeaders(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? StyledOutput(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Stderr(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Sslv3(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Sslv2(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? SslReqd(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? SslNoRevoke(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? SslAllowBeast(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Ssl(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? SpeedTime(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? SpeedLimit(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Socks5Hostname(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Socks5GssapiService(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Socks5GssapiNec(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Socks5Gssapi(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Socks5Basic(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Socks5(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Socks4a(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Socks4(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Silent(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ShowError(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? ServiceName(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? SaslIr(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? RetryMaxTime(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? RetryRelay(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? RetryConnrefused(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Retry(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? Resolve(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? RequestTarget(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? RemoteTime(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? RemoteNameAll(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

        //internal static CurlInterpreterAction? RemoteName(ArgumentSource arguments)
        //{
        //    Stop();
        //    Action<CurlInterpreterAction, CurlContext> action = (sender, context) =>
        //     {
        //         Stop();

        //     };

        //    return new CurlInterpreterAction(action) { Priority = 20 };
        //}

    }


}
