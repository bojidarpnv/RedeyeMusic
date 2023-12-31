# Redeye Music Web Application

## Project Overview
The Redeye Music Web Application is a platform for music enthusiasts to discover and listen to songs, manage playlists, and explore artists and albums.

## Getting Started
To successfully run the Redeye Music Web Application, follow the steps below.

### Prerequisites
Before you begin, ensure you have the following software installed:

- Visual Studio (2019 or later)
- SQL Server (or SQL Server Express)

### Step 1: Clone the Repository
Clone this repository to your local machine using the following command:

``bash
git clone https://github.com/your-username/RedeyeMusic.git``

## Step 2: Configure Connection Strings

1. Open the RedeyeMusic.Web project in Visual Studio.
2. Locate the `appsettings.json` file in the RedeyeMusic.Web project.
3. Update the connection string in the `appsettings.json` file to match your SQL Server configuration.

## Step 3: Set Multiple Startup Projects

1. Right-click on the solution (`RedeyeMusic.sln`) in the Solution Explorer and select "Properties."
2. In the "Common Properties" section, navigate to "Startup Project."
3. Select "Multiple startup projects."
4. Set the action to "Start" for both RedeyeMusic.Web and RedeyeMusic.WebApi projects.
5. Click "Apply" and then "OK" to save the changes.

## Step 4: Build and Run

1. Build the solution by selecting "Build" > "Build Solution" from the Visual Studio menu.
2. Press F5 or click the "Start" button to run the application.
3. The browser will open, and you'll be redirected to the Redeye Music Web Application.

## Step 5: Explore the Application

Explore the various features of the application, such as discovering songs, managing playlists, and exploring artists and albums.

## Feedback and Support

If you encounter any issues or have questions about the Redeye Music Web Application, please feel free to reach out to me at bojidarpenev@gmail.com.