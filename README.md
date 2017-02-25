# PaperID

## Getting Started On Mac

### Install Prerequisites

1. Install latest version of  OpenSSL
2. Download and install [.NET Core SDK 1.0 rc4 build 004771](https://github.com/dotnet/core/blob/master/release-notes/rc4-download.md).
3. (Optional/Recommended): Download and install [Visual Studio Code](https://code.visualstudio.com).

### Run the code

From the directory `src/PaperID/src/PaperID`, run following commands in the console:
1. `dotnet restore`  (Only need to run once)
2. `dotnet run`(Starts the app)
3. Navigate to the directory listed in the console.

### Edit code

Code is found under `src/PaperID/src/PaperID`.

Within that folder, the code for the dashboard can be found under:
* CSS: `wwwroot/css/Dashboard.css` and `wwwroot/css/site.css`
* JS: `wwwroot/js/DashboardViewModel.JS`
* View: `Views/Dashboard/Index.cshtml`