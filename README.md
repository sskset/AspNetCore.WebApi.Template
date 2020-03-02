# Tutorial:

## AspNetCore.WebApi.Template project

1. Using Visual Studio 2019 to open ./AspNetCore.WebApi.Template/AspNetCore.WebApi.Template.sln  
   1.1 If you want to update the WebApi template, update this project
2. Click Visual Studio Menu [Proejcts] -> [Export Template] and choose [Project template], and all the way to Finish
3. As it's unable to change the Output location in previous step, here it's been saved to  
   C:\Users\[user]\Documents\Visual Studio 2019\My Exported Templates\AspNetCore.WebApi.Template.zip

## AspNetCore.WebApi.Template.VSIX project

4. Using Visual Studio 2019 to open ./AspNetCore.WebApi.Template.VSIX/AspNetCore.WebApi.Template.VSIX.sln
5. Add the output file AspNetCore.WebApi.Template.zip in step 3 into AspNetCore.WebApi.Template.VSIX project
6. In the properties window of AspNetCore.WebApi.Template.zip, set [Build Action] to Content and [Copy to Output Directoy] to Copy always
7. Build
8. In the output folder, we can find the .vsix file and double-click to install into your Visual Studio
