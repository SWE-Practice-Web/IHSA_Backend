# IHSA_Backend

## Quickstart
Download your firestore service account key and place it somewhere safe on your computer. We will override the appsettings path to point to this file. Also take note of the `project-id`.

```bash
git clone git@github.com:SWE-Practice-Web/IHSA_Backend.git
cd IHSA_Backend
dotnet user-secrets init
dotnet user-secrets set "GoogleApplicationCredentialsPath" "<path/to/adminsdk.json>"
dotnet user-secrets set "FirestoreProjectId" "<project-id>"
dotnet run
```