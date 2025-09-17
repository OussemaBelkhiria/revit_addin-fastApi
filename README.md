
# Revit add-in 
In this project, I created a custom tab named "Test", which loads automatically when Revit starts. This tab contains a ribbon panel called "Demo", which includes a button labeled "Show Panel". When clicked, this button opens a dockable pane titled "Dockable Pane", which hosts a WPF-based user interface.

The request area of the pane includes a predefined input format. After filling it out and clicking the button, the request (POST) is sent to a server built with FastAPI in Python, running locally on port 8000. If the request is successful, the response area will display the server’s response.



## Requirements
- Revit 2026
- Visual Studio 2022 for creating the addin.
- Python incl. FastAPI and uvicorn for creating and running the server


## Add the AddIn to Revit
- Open visual studio : File -> open -> Project/Solution and navigate to the folder where you cloned the project and then select RevitAddin.sln which is found in src/RevitAddin
- If Revit 2026 is not installed on the path : C:\Program Files\Autodesk\Revit 2026 then :
        solution explorer -> RevitAddin -> Dependencies -> Add Project Reference -> Browse 
        and add both of RevitAPI.dll , RevitAPIUI.dll which are found in the installation folder of Revit
- Build the solution 


    
## Set up the RevitAddin.addin file

- The file is delivered with the project
- In the section "Assembly" you need to specify where the RevitAddin.dll file is after building the project. Example: 
  C:\Users\msi\Desktop\Task\src\RevitAddin\RevitAddin\bin\Debug\net8.0-windows\RevitAddin.dll
- Copy RevitAddin.addin to "C:\ProgramData\Autodesk\Revit\Addins\2026"
- Doing that you specify to Revit where it can find the .dll file containing the addin's logic

## Starting the server
- Using the terminal navigate to the fastapi folder of the project and run : 
        uvicorn server:app --reload --host localhost --port 8000
    or 
        py -m uvicorn server:app --reload --host localhost --port 8000
- Open Revit and test the addin!