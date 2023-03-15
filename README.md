# IHSA_Backend

## Quickstart - docker
Make sure you have docker desktop installed and you can access it with powershell or cmd. Copy the `example.env` to a new file `.env` and fill in the the blank variables (`GOOGLE_APPLICATION_CREDENTIALS_BASE64` and `FIRESTORE_PROJECT_ID`) with values from your firestore service account key.

*Note: You can get the base64 encoded string by running `cat <path/to/adminsdk.json> | base64` in WSL or by using some online tool*

Run the following commands to build and run the docker container:

```bash

docker build -t ihsabackend:dev .
docker run --env-file=.env --volume=C:\Users\<username>\AppData\Roaming\Microsoft\UserSecrets:/root/.microsoft/usersecrets:ro --volume=C:\Users\<username>\AppData\Roaming\ASP.NET\Https:/root/.aspnet/https:ro --workdir=/app -p 32772:443 -p 32773:80 --restart=no --label='com.microsoft.visual-studio.project-name=IHSA_Backend' --runtime=runc -t -d ihsabackend:dev
```

The swagger pages will be hosted on https://localhost:32772/swagger/index.html and http://localhost:32773/swagger/index.htmls.

If you run into any issues, try running the following commands to clean up all docker containers & images and try again:

```bash
docker system prune
```

## Quickstart - old
Download your firestore service account key and place it somewhere safe on your computer. We will override the appsettings path to point to this file. Also take note of the `project-id`.

```bash
git clone git@github.com:SWE-Practice-Web/IHSA_Backend.git
cd IHSA_Backend
dotnet user-secrets init
dotnet user-secrets set "GoogleApplicationCredentialsPath" "<path/to/adminsdk.json>"
dotnet user-secrets set "FirestoreProjectId" "<project-id>"
dotnet run
```