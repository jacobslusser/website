namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => """
                Wait, what?, 4/15/2024

                If you have found this website in its present state, you may be wondering, what is going on? At the moment, not much! LOL.
                Let me explain. Right now, there is no website. Nothing. This is (nearly) the most minimal web project that can exist.
                There is no database, there is no static site generator, there isn't even HTML yet -- just plain text.

                In a way, this is the first of many ideas I would like to share, the website itself. Rather than just use an existing technology
                or blog platform, we will build one! Any mistakes and things I do along the way you will get to see also. It is also a
                lesson about "just starting". So often, I can get caught up in overplanning things and so this approach forces me to "just start",
                even though I don't know how it will all go yet.

                So right now, I am writing this in a code editor -- not a nice UI, no spellcheck, nothing. I created this project using
                Visual Studio > New Project > ASP.NET Core Empty and the posts are literally compiled into the executable and deployed any time
                I want to update. I haven't even named the project. It still has the default name of WebApplication1.

                My update process looks like this:
                1. I make my changes in the code,
                2. Run the following to compile into a docker image: `dotnet publish -t:PublishContainer`,
                3. Export that docker image to a tar file using: `docker save -o ./webapplication1.tar webapplication1:latest`,
                4. Copy that to my host Linux machine: `scp webapplication1.tar jacob@192.168.4.81:/home/jacob/website`,
                5. Import the docker image to the local registry: `sudo docker load -i ./webapplication1.tar`,
                6. And finally recompose the docker container: `sudo docker compose up -d`.

                It's a whole thing...

                You can see this in the source code which I am also sharing here: https://github.com/jacobslusser/website.

                ---

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
