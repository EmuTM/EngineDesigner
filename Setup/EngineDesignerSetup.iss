; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Engine designer"
#define MyAppVersion "1.1.2"
#define MyAppPublisher "Bla� Umek"
#define MyAppURL "Facebook: Engine designer"
#define MyAppExeName "EngineDesigner.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{CEBA570B-D4DA-48AF-8EF5-BD6750A0649E}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\EngineDesigner
DefaultGroupName={#MyAppName}
InfoBeforeFile=D:\Google Drive\EngineDesigner\Setup\BeforeInstallationNote.rtf
LicenseFile=D:\Google Drive\EngineDesigner\Setup\License.rtf
OutputBaseFilename=EngineDesignerSetup
SetupIconFile=D:\Google Drive\EngineDesigner\Resources\Logo_Pistons.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\Google Drive\EngineDesigner\EngineDesigner\bin\Release\EngineDesigner.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Google Drive\EngineDesigner\EngineDesigner\bin\Release\EngineDesigner.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Google Drive\EngineDesigner\EngineDesigner\bin\Release\EngineDesigner.chm"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Google Drive\EngineDesigner\EngineDesigner\bin\Release\EngineDesigner.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Google Drive\EngineDesigner\EngineDesigner\bin\Release\EngineDesigner.Common.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Google Drive\EngineDesigner\EngineDesigner\bin\Release\EngineDesigner.Common.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Google Drive\EngineDesigner\EngineDesigner\bin\Release\EngineDesigner.Environment.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Google Drive\EngineDesigner\EngineDesigner\bin\Release\EngineDesigner.Environment.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Google Drive\EngineDesigner\EngineDesigner\bin\Release\EngineDesigner.Machine.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Google Drive\EngineDesigner\EngineDesigner\bin\Release\EngineDesigner.Machine.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Google Drive\EngineDesigner\EngineDesigner\bin\Release\EngineDesigner.Media.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Google Drive\EngineDesigner\EngineDesigner\bin\Release\EngineDesigner.Media.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Google Drive\EngineDesigner\EngineDesigner\bin\Release\SlimDX.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Google Drive\EngineDesigner\EngineDesigner\bin\Release\SlimDX.pdb"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

