
using RestSharp;

namespace Bb.Curls
{

    /// <summary>
    /// Convert curl syntax to http request
    /// </summary>
    public partial class CurlInterpreter
    {

        // https://reqbin.com/req/c-bjcj04uw/curl-send-cookies-example

        static CurlInterpreter()
        {

            _parameters = new Dictionary<string, Func<ArgumentSource, CurlInterpreterAction>>();
            Append(Request, "--request", "-X");                             // <command> Specify request command to use
            Append(Header, "--header", "-H");                               // <header/@file> Pass custom header(s) to server
            Append(Form, "--form", "-F");                                   // <name=content> Specify multipart MIME data
            Append(Data, "--data", "-d");                                   // <data>   HTTP POST data
            Append(Cookie, "--cookie", "-b");                               // <data|filename> Send cookies from string/file
            Append(CookieJar, "--cookie-jar","-c");                         // <filename> Write cookies to <filename> after operation
            Append(User, "--user", "-u");                                   // <user:password> Server user and password
            //Append(Oauth2Bearer, "--oauth2-bearer");                        // <token> OAuth 2 Bearer Token
            
            //Append(AppendMethod, "--append", "-a");                         // Append to target file when uploading
            //Append(Basic, "--basic");                                       // Use HTTP Basic Authentication
            //Append(CaCert, "--cacert");                                     // <file> CA certificate to verify peer against
            //Append(Capath, "--capath");                                     // <dir>  CA directory to verify peer against
            //Append(Cert, "--cert", "-E");                                   // <certificate[:password]> Client certificate file and password
            //Append(CertStatus, "--cert-status");                            // Verify the status of the server certificate
            //Append(CertStatus, "--cert-status");                            // Verify the status of the server certificate
            //Append(CertType, "--cert-type");                                // <type> Certificate file type (DER/PEM/ENG)
            //Append(Ciphers, "--ciphers");                                   // <list of ciphers> SSL ciphers to use
            //Append(Compressed, "--compressed");                             // Request compressed response
            //Append(CompressedSsh, "--compressed-ssh");                      // Enable SSH compression
            //Append(Config, "--config", "-K");                               // <file> Read config from a file
            //Append(ConnectTimeout, "--connect-timeout");                    // <seconds> Maximum time allowed for connection
            //Append(ConnectTo, "--connect-to");                              // <HOST1:PORT1:HOST2:PORT2> Connect to host
            //Append(ContinueAt, "--continue-at", "-C");                      // <offset> Resumed transfer offset            
            //Append(CreateDirs, "--create-dirs");                            // Create necessary local directory hierarchy
            //Append(Crlf, "--crlf");                                         // Convert LF to CRLF in upload
            //Append(Crlfile, "--crlfile");                                   // <file> Get a CRL list in PEM format from the given file
            //Append(DataAscii, "--data-ascii");                              // <data> HTTP POST ASCII data
            //Append(DataBinary, "--data-binary");                            // <data> HTTP POST binary data
            //Append(DataRaw, "--data-raw");                                  // <data> HTTP POST data, '@' allowed
            //Append(DataUrlencode, "--data-urlencode");                      // <data> HTTP POST data url encoded
            //Append(Delegation, "--delegation");                             // <LEVEL> GSS-API delegation permission
            //Append(Digest, "--digest");                                     // Use HTTP Digest Authentication
            //Append(Disable, "--disable", "-q");                             // Disable .curlrc
            //Append(DisableEprt, "--disable-eprt");                          // Inhibit using EPRT or LPRT
            //Append(DisableEpsv, "--disable-epsv");                          // Inhibit using EPSV
            //Append(DisallowUsernameInUrl, "--disallow-username-in-url");    // Disallow username in url
            //Append(DnsInterface, "--dns-interface");                        // <interface> Interface to use for DNS requests
            //Append(DnsIpv4Addr, "--dns-ipv4-addr");                         // <address> IPv4 address to use for DNS requests
            //Append(DnsIpv6Addr, "--dns-ipv6-addr");                         // <address> IPv6 address to use for DNS requests
            //Append(DnsServers, "--dns-servers");                            // <addresses> DNS server addrs to use
            //Append(DohUrl, "--doh-url");                                    // <URL> Resolve host names over DOH
            //Append(DumpHeader, "--dump-header", "-D");                      // <filename> Write the received headers to <filename>
            //Append(EgdFile, "--egd-file");                                  // <file> EGD socket path for random data
            //Append(Engine, "--engine");                                     // <name> Crypto engine to use
            //Append(Expect100Timeout, "--expect100-timeout");                // <seconds> How long to wait for 100-continue
            //Append(Fail, "--fail", "-f");                                    // Fail silently (no output at all) on HTTP errors
            //Append(FailEarly, "--fail-early");                              // Fail on first transfer error, do not continue
            //Append(FalseStart, "--false-start");                            // Enable TLS False Start
            //Append(FormString, "--form-string");                            // <name=string> Specify multipart MIME data
            //Append(FtpAccount, "--ftp-account");                            // <data> Account data string
            //Append(FtpAlternativeToUser, "--ftp-alternative-to-user");      // <command> String to replace USER [name]
            //Append(FtpCreateDirs, "--ftp-create-dirs");                     // Create the remote dirs if not present
            //Append(FtpMethod, "--ftp-method");                              // <method> Control CWD usage
            //Append(FtpPasv, "--ftp-pasv");                                  // Use PASV/EPSV instead of PORT
            //Append(FtpPort, "--ftp-port", "-P");                            // <address> Use PORT instead of PASV
            //Append(FtpPret, "--ftp-pret");                                  // Send PRET before PASV
            //Append(FtpSkipPasvIp, "--ftp-skip-pasv-ip");                    // Skip the IP address for PASV
            //Append(FtpSslCcc, "--ftp-ssl-ccc");                             // Send CCC after authenticating
            //Append(FtpSslCccMode, "--ftp-ssl-ccc-mode");                    // <active/passive> Set CCC mode
            //Append(FtpSslControl, "--ftp-ssl-control");                     // Require SSL/TLS for FTP login, clear for transfer
            //Append(Get, "--get", "-G");                                     // Put the post data in the URL and use GET
            //Append(Globoff, "--globoff", "-g");                             // Disable URL sequences and ranges using {} and []
            //Append(HappyEyeballsTimeoutMs, "--happy-eyeballs-timeout-ms");  // <milliseconds> How long to wait in milliseconds for IPv6 before trying IPv4
            //Append(HaproxyProtocol, "--haproxy-protocol");                  // Send HAProxy PROXY protocol v1 header
            //Append(Head, "--head", "-I");                                   // Show document info only
            //Append(Help, "--help", "-h");                                   // This help text
            //Append(Hostpubmd5, "--hostpubmd5");                             // <md5> Acceptable MD5 hash of the host public key
            //Append(Http09, "--http0.9");                                    // Allow HTTP 0.9 responses
            //Append(Http10, "--http1.0", "-0");                              // Use HTTP 1.0
            //Append(Http11, "--http1.1");                                    // Use HTTP 1.1
            //Append(Http2, "--http2");                                       // Use HTTP 2
            //Append(Http2PriorKnowledge, "--http2-prior-knowledge");         // Use HTTP 2 without HTTP/1.1 Upgrade
            //Append(IgnoreContentLength, "--ignore-content-length");         // Ignore the size of the remote resource
            //Append(Include, "--include", "-i");                             // Include protocol response headers in the output
            //Append(Insecure, "--insecure", "-k");                           // Allow insecure server connections when using SSL
            ////Append(Interface, "--interface");                             // <name> Use network INTERFACE (or address)
            //Append(Ipv4, "--ipv4", "-4");                                   // Resolve names to IPv4 addresses
            //Append(Ipv6, "--ipv6", "-6");                                   // Resolve names to IPv6 addresses
            //Append(JunkSessionCookies, "--junk-session-cookies", "-j");     // Ignore session cookies read from file
            //Append(KeepaliveTime, "--keepalive-time");                      // <seconds> Interval time for keepalive probes
            //Append(Key, "--key");                                           // <key>     Private key file name
            //Append(KeyType, "--key-type");                                  // <type> Private key file type (DER/PEM/ENG)
            //Append(Krb, "--krb");                                           // <level>   Enable Kerberos with security <level>
            //Append(Libcurl, "--libcurl");                                   // <file> Dump libcurl equivalent code of this command line
            //Append(LimitRate, "--limit-rate");                              // <speed> Limit transfer speed to RATE
            //Append(ListOnly, "--list-only", "-l");                          // List only mode
            //Append(LocalPort, "--local-port");                              // <num/range> Force use of RANGE for local port numbers
            //Append(Location, "--location", "-L");                           // Follow redirects
            //Append(LocationTrusted, "--location-trusted");                  // Like --location, and send auth to other hosts
            //Append(LoginOptions, "--login-options");                        // <options> Server login options
            //Append(MailAuth, "--mail-auth");                                // <address> Originator address of the original email
            //Append(MailFrom, "--mail-from");                                // <address> Mail from this address
            //Append(MailRcpt, "--mail-rcpt");                                // <address> Mail to this address
            //Append(Manual, "--manual", "-M");                               // Display the full manual
            //Append(MaxFilesize, "--max-filesize");                          // <bytes> Maximum file size to download
            //Append(MaxRedirs, "--max-redirs");                              // <num> Maximum number of redirects allowed
            //Append(MaxTime, "--max-time", "-m");                            // <seconds> Maximum time allowed for the transfer
            //Append(Metalink, "--metalink");                                 // Process given URLs as metalink XML file
            //Append(Negotiate, "--negotiate");                               // Use HTTP Negotiate (SPNEGO) authentication
            //Append(Netrc, "--netrc", "-n");                               // Must read .netrc for user name and password
            //Append(NetrcFile, "--netrc-file");                              // <filename> Specify FILE for netrc
            //Append(NetrcOptional, "--netrc-optional");                      // Use either .netrc or URL
            //Append(Next, "--next", "-:");                                   // Make next URL use its separate set of options
            //Append(NoAlpn, "--no-alpn");                                    // Disable the ALPN TLS extension
            //Append(NoBuffer, "--no-buffer", "-N");                          // Disable buffering of the output stream
            //Append(NoKeepalive, "--no-keepalive");                          // Disable TCP keepalive on the connection
            //Append(NoNpn, "--no-npn");                                      // Disable the NPN TLS extension
            //Append(NoSessionid, "--no-sessionid");                          // Disable SSL session-ID reusing
            //Append(Noproxy, "--noproxy");                                   // <no-proxy-list> List of hosts which do not use proxy
            //Append(Ntlm, "--ntlm");                                         // Use HTTP NTLM authentication
            //Append(NtlmWb, "--ntlm-wb");                                    // Use HTTP NTLM authentication with winbind
            //Append(Output, "--output", "-o");                               // <file> Write to file instead of stdout
            //Append(Pass, "--pass");                                         // <phrase> Pass phrase for the private key
            //Append(PathAsIs, "--path-as-is");                               // Do not squash .. sequences in URL path
            //Append(Pinnedpubkey, "--pinnedpubkey");                         // <hashes> FILE/HASHES Public key to verify peer against
            //Append(Post301, "--post301");                                   // Do not switch to GET after following a 301
            //Append(Post302, "--post302");                                   // Do not switch to GET after following a 302
            //Append(Post303, "--post303");                                   // Do not switch to GET after following a 303
            //Append(Preproxy, "--preproxy");                                 // [protocol://]host[:port] Use this proxy first
            //Append(ProgressBar, "--progress-bar", "-#");                    // Display transfer progress as a bar
            //Append(Proto, "--proto");                                       // <protocols> Enable/disable PROTOCOLS
            //Append(ProtoDefault, "--proto-default");                        // <protocol> Use PROTOCOL for any URL missing a scheme
            //Append(ProtoRedir, "--proto-redir");                            // <protocols> Enable/disable PROTOCOLS on redirect
            //Append(Proxy, "--proxy", "-x");                                 // [protocol://]host[:port] Use this proxy
            //Append(ProxyAnyauth, "--proxy-anyauth");                        // Pick any proxy authentication method
            //Append(ProxyBasic, "--proxy-basic");                            // Use Basic authentication on the proxy
            //Append(ProxyCacert, "--proxy-cacert");                          // <file> CA certificate to verify peer against for proxy
            //Append(ProxyCapath, "--proxy-capath");                          // <dir> CA directory to verify peer against for proxy
            //Append(ProxyCert, "--proxy-cert");                              // <cert[:passwd]> Set client certificate for proxy
            //Append(ProxyCertType, "--proxy-cert-type");                     // <type> Client certificate type for HTTPS proxy
            //Append(ProxyCiphers, "--proxy-ciphers");                        // <list> SSL ciphers to use for proxy
            //Append(ProxyCrlfile, "--proxy-crlfile");                        // <file> Set a CRL list for proxy
            //Append(ProxyDigest, "--proxy-digest");                          // Use Digest authentication on the proxy
            //Append(ProxyHeader, "--proxy-header");                          // <header/@file> Pass custom header(s) to proxy
            //Append(ProxyInsecure, "--proxy-insecure");                      // Do HTTPS proxy connections without verifying the proxy
            //Append(ProxyKey, "--proxy-key");                                // <key> Private key for HTTPS proxy
            //Append(ProxyKeyType, "--proxy-key-type");                       // <type> Private key file type for proxy
            //Append(ProxyNegotiate, "--proxy-negotiate");                    // Use HTTP Negotiate (SPNEGO) authentication on the proxy
            //Append(ProxyNtlm, "--proxy-ntlm");                              // Use NTLM authentication on the proxy
            //Append(ProxyPass, "--proxy-pass");                              // <phrase> Pass phrase for the private key for HTTPS proxy
            //Append(ProxyPinnedpubkey, "--proxy-pinnedpubkey");              // <hashes> FILE/HASHES public key to verify proxy with
            //Append(ProxyServiceName, "--proxy-service-name");               // <name> SPNEGO proxy service name
            //Append(ProxySslAllowBeast, "--proxy-ssl-allow-beast");          // Allow security flaw for interop for HTTPS proxy
            //Append(ProxyTls13Ciphers, "--proxy-tls13-ciphers");             // <ciphersuite list> TLS 1.3 proxy cipher suites
            //Append(ProxyTlsauthtype, "--proxy-tlsauthtype");                // <type> TLS authentication type for HTTPS proxy
            //Append(ProxyTlspassword, "--proxy-tlspassword");                // <string> TLS password for HTTPS proxy
            //Append(ProxyTlsuser, "--proxy-tlsuser");                        // <name> TLS username for HTTPS proxy
            //Append(ProxyTlsv1, "--proxy-tlsv1");                            // Use TLSv1 for HTTPS proxy
            //Append(ProxyUser, "--proxy-user", "-U");                        // <user:password> Proxy user and password
            //Append(Proxy10, "--proxy1.0");                                  // <host[:port]> Use HTTP/1.0 proxy on given port
            //Append(Proxytunnel, "--proxytunnel", "-p");                     // Operate through an HTTP proxy tunnel (using CONNECT)
            //Append(Pubkey, "--pubkey");                                     // <key>  SSH Public key file name
            //Append(Quote, "--quote", "-Q");                                 // Send command(s) to server before transfer
            //Append(RandomFile, "--random-file");                            // <file> File for reading random data from
            //Append(Range, "--range", "-r");                                 // <range> Retrieve only the bytes within RANGE
            //Append(Raw, "--raw");                                           // Do HTTP "raw"; no transfer decoding
            //Append(Referer, "--referer", "-e");
            //Append(RemoteHeaderName, "--remote-header-name", "-J");         // Use the header-provided filename
            //Append(RemoteName, "--remote-name", "-O");                      // Write output to a file named as the remote file
            //Append(RemoteNameAll, "--remote-name-all");                     // Use the remote file name for all URLs
            //Append(RemoteTime, "--remote-time", "-R");                      // Set the remote file's time on the local output
            //Append(RequestTarget, "--request-target");                      // Specify the target for this request
            //Append(Resolve, "--resolve");                                   // <host:port:address[,address]...> Resolve the host+port to this address
            //Append(Retry, "--retry");                                       // <num>   Retry request if transient problems occur
            //Append(RetryConnrefused, "--retry-connrefused");                // Retry on connection refused (use with --retry)
            //Append(RetryRelay, "--retry-delay");                            // <seconds> Wait time between retries
            //Append(RetryMaxTime, "--retry-max-time");                       // <seconds> Retry only within this period
            //Append(SaslIr, "--sasl-ir");                                    // Enable initial response in SASL authentication
            //Append(ServiceName, "--service-name");                          // <name> SPNEGO service name
            //Append(ShowError, "--show-error", "-S");                        // Show error even when -s is used
            //Append(Silent, "--silent", "-s");                               // Silent mode
            //Append(Socks4, "--socks4");                                     // <host[:port]> SOCKS4 proxy on given host + port
            //Append(Socks4a, "--socks4a");                                   // <host[:port]> SOCKS4a proxy on given host + port
            //Append(Socks5, "--socks5");                                     // <host[:port]> SOCKS5 proxy on given host + port
            //Append(Socks5Basic, "--socks5-basic");                          // Enable username/password auth for SOCKS5 proxies
            //Append(Socks5Gssapi, "--socks5-gssapi");                        // Enable GSS-API auth for SOCKS5 proxies
            //Append(Socks5GssapiNec, "--socks5-gssapi-nec");                 // Compatibility with NEC SOCKS5 server
            //Append(Socks5GssapiService, "--socks5-gssapi-service");         // <name> SOCKS5 proxy service name for GSS-API
            //Append(Socks5Hostname, "--socks5-hostname");                    // <host[:port]> SOCKS5 proxy, pass host name to proxy
            //Append(SpeedLimit, "--speed-limit", "-Y");                      // <speed> Stop transfers slower than this
            //Append(SpeedTime, "--speed-time", "-y");                        // <seconds> Trigger 'speed-limit' abort after this time
            //Append(Ssl, "--ssl");                                           // Try SSL/TLS
            //Append(SslAllowBeast, "--ssl-allow-beast");                     // Allow security flaw to improve interop
            //Append(SslNoRevoke, "--ssl-no-revoke");                         // Disable cert revocation checks (Schannel)
            //Append(SslReqd, "--ssl-reqd");                                  // Require SSL/TLS
            //Append(Sslv2, "--sslv2", "-2");                                 // Use SSLv2
            //Append(Sslv3, "--sslv3", "-3");                                 // Use SSLv3
            //Append(Stderr, "--stderr");                                     // Where to redirect stderr
            //Append(StyledOutput, "--styled-output");                        // Enable styled output for HTTP headers
            //Append(SuppressConnectHeaders, "--suppress-connect-headers");   // Suppress proxy CONNECT response headers
            //Append(TcpFastopen, "--tcp-fastopen");                          // Use TCP Fast Open
            //Append(TcpNodelay, "--tcp-nodelay");                            // Use the TCP_NODELAY option
            //Append(TelnetOption, "--telnet-option", "-t");                  // <opt=val> Set telnet option
            //Append(TftpBlksize, "--tftp-blksize");                          // <value> Set TFTP BLKSIZE option
            //Append(TftpNoOptions, "--tftp-no-options");                     // Do not send any TFTP options
            //Append(TimeCond, "--time-cond", "-z");                          // <time> Transfer based on a time condition
            //Append(TlsMax, "--tls-max");                                    // <VERSION> Set maximum allowed TLS version
            //Append(Tls13Ciphers, "--tls13-ciphers");                        // <list of TLS 1.3 ciphersuites> TLS 1.3 cipher suites to use
            //Append(Tlsauthtype, "--tlsauthtype");                           // <type> TLS authentication type
            //Append(Tlspassword, "--tlspassword");                           // TLS password
            //Append(Tlsuser, "--tlsuser");                                   // <name> TLS user name
            //Append(Tlsv1, "--tlsv1", "-1");                                 // Use TLSv1.0 or greater
            //Append(Tlsv10, "--tlsv1.0");                                    // Use TLSv1.0 or greater
            //Append(Tlsv11, "--tlsv1.1");                                    // Use TLSv1.1 or greater
            //Append(Tlsv12, "--tlsv1.2");                                    // Use TLSv1.2 or greater
            //Append(Tlsv13, "--tlsv1.3");                                    // Use TLSv1.3 or greater
            //Append(TrEncoding, "--tr-encoding");                            // Request compressed transfer encoding
            //Append(Trace, "--trace");                                       // <file>  Write a debug trace to FILE
            //Append(TraceAscii, "--trace-ascii");                            // <file> Like --trace, but without hex output
            //Append(TraceTime, "--trace-time");                              // Add time stamps to trace/verbose output
            //Append(UnixSocket, "--unix-socket");                            // <path> Connect through this Unix domain socket
            //Append(UploadFile, "--upload-file", "-T");                      // <file> Transfer local FILE to destination
            //Append(Url, "--url");                                           // <url> URL to work with
            //Append(UseAscii, "--use-ascii", "-B");                          // Use ASCII/text transfer
            //Append(UserAgent, "--user-agent", "-A");                        // <name> Send User-Agent <name> to server
            //Append(Verbose, "--verbose", "-v");                             // Make the operation more talkative
            //Append(Version, "--version", "-V");                             // Show version number and quit
            //Append(WriteOut, "--write-out", "-w");                          // <format> Use output FORMAT after completion
            //Append(Xattr, "--xattr");                                       // Store metadata in extended file attributes
            //Append(AbstractUnixSocket, "--abstract-unix-socket");           // <path> Connect via abstract Unix domain socket
            //Append(AltSvc, "--alt-svc");                                    // <file name> Enable alt-svc with this cache file
            //Append(Anyauth, "--anyauth");                                   // Pick any authentication method

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurlInterpreter"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public CurlInterpreter(string[] arguments)
        {
            _arguments = new ArgumentSource(arguments);
            _handlers = new List<CurlInterpreterAction>();
        }


        ///// <summary>
        ///// Calls asynchronously.
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="ensureSuccessStatusCode">if true thrown an exception if the result of the call is not 2xx</param>
        ///// <returns></returns>
        //public async Task<RestResponse?> CallAsync(bool ensureSuccessStatusCode, CancellationTokenSource ? source = null)
        //{

        //    if (BuildActions())
        //    {
        //        var context = new CurlContext(source) // { RequestMessage = message, }
        //            .Apply(_list);

        //        context.Request.EnsureSuccessStatusCode = ensureSuccessStatusCode;

        //        this.LastResponse = await context.CallAsync(UrlHttp.GlobalSettings.UrlClientFactory);
        //        return this.LastResponse;
        //    }

        //    return default;

        //}

        public bool IsFailed { get; private set; }

        public string ResultMessage { get; private set; }

        public RestResponse LastResponse { get; internal set; }


        /// <summary>
        /// implicit conversion from string to CurlInterpreter
        /// </summary>
        /// <param name="self"></param>
        public static implicit operator CurlInterpreter(string self)
        {
            return self.Precompile();
        }


        internal bool BuildActions()
        {

            if (_list == null && _arguments.CanRead)
                lock (_lock)
                    if (_list == null)
                    {

                        ParseArguments();

                        if (_arguments.IsFailed)
                        {
                            this.IsFailed = true;
                            this.ResultMessage = _arguments.FailMessage;
                        }
                        else
                            this._list = _handlers.OrderBy(c => c.Priority).ToList();
                    }

            return _list != null;

        }


        protected static void Append(Func<ArgumentSource, CurlInterpreterAction?> action, params string[] keys)
        {
            foreach (var item in keys)
            {
                if (_parameters.ContainsKey(item))
                    _parameters[item] = action;
                else
                    _parameters.Add(item, action);
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        protected static void Stop()
        {
            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Break();
        }


        private void Append(CurlInterpreterAction? curlInterpreterAction)
        {
            if (curlInterpreterAction != null && !_arguments.IsFailed)
                _handlers.Add(curlInterpreterAction);
        }

        private void ParseArguments()
        {

            bool uri = false;
            while (_arguments.ReadNext())
            {

                string current = this._arguments.Current;

                if (_parameters.TryGetValue(current, out var parameters))
                    Append(parameters(this._arguments));

                else
                {
                    if (!uri)
                    {
                        _isUrl = current.IsUrl();
                        if (_isUrl)
                        {
                            Append(SetUrl(this._arguments));
                            uri = true;
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }

            }

        }

        private readonly List<CurlInterpreterAction> _handlers;
        private volatile object _lock = new object();
        private bool _isUrl;
        private static Dictionary<string, Func<ArgumentSource, CurlInterpreterAction>> _parameters;
        private List<CurlInterpreterAction> _list;
        private readonly ArgumentSource _arguments;

    }

}
