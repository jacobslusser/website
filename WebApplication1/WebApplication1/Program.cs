namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => """
                Humble Beginnings, 4/14/2024

                Join me as I build a personal website from scratch and share some things that I've learned in my life....

                Source for the website is here:
                https://github.com/jacobslusser/website

                Current stack:
                - ASP.NET Core 8 SDK Container
                - Selfhosted on Ubuntu Server 22.04
                - Docker Compose
                - Intel i5-6600T, 8GB of RAM
                - Cloudflare Tunnel Proxy

                Helpful resources:
                - Building container apps in .NET - https://youtu.be/scIAwLrruMY?si=X5ujWTBHb8RxkKmv
                - Copying the image to another server - https://stackoverflow.com/questions/23935141/how-to-copy-docker-images-from-one-host-to-another-without-using-a-repository
                """
            );

            app.Run();
        }
    }
}
