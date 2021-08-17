public static async Task<string> Http1_Get_With_Proxy(string url1)
{
    string html_content = "";
    try
    {
    WebProxy proxy = new WebProxy
    {
    Address = new Uri($"http://1.2.3.4:8888"),
    };
    ServicePointManager.Expect100Continue = false;
    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
    HttpClientHandler clientHandler = new HttpClientHandler()
    {
    AllowAutoRedirect = true,
    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
    Proxy = proxy,
    };
    using HttpClient client1 = new HttpClient(clientHandler);
    client1.DefaultRequestHeaders.Accept.Clear();
    client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
    client1.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url1);
    HttpResponseMessage response = await client1.SendAsync(request);
    if (response.StatusCode == HttpStatusCode.OK)
    {
    html_content = await response.Content.ReadAsStringAsync();
    }
    }
    catch (HttpRequestException ex)
    {
    Console.WriteLine(ex.Message);
    }
    return (html_content);
}
