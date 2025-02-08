# Save this as GridCraftTools.ps1

function Run-DotnetWithHttps {
    Write-Host "Running dotnet with HTTPS profile..."
    dotnet run --launch-profile https
}

function Create-ApiController {
    param (
        [string]$ControllerName
    )

    if (-not $ControllerName) {
        Write-Host "Error: Please provide a controller name."
        return
    }

    $FolderName = "$ControllerName"
    $ControllerFilePath = "$FolderName\$ControllerName" + "Controller.cs"

    # Create the directory and controller
    New-Item -ItemType Directory -Path $FolderName -Force | Out-Null
    dotnet new apicontroller -n "${ControllerName}Controller" -o $FolderName

    # Update namespace to the desired one
    (Get-Content $ControllerFilePath) -replace "namespace MyApp.Namespace", "namespace GridCraftTableGenDotNetWebApi.$ControllerName" | Set-Content $ControllerFilePath

    Write-Host "Controller '${ControllerName}Controller' created in $FolderName with namespace GridCraftTableGenDotNetWebApi.$ControllerName."
}

function Add-DotnetPackage {
    param (
        [string]$PackageName
    )

    if (-not $PackageName) {
        Write-Host "Error: Please provide a package name."
        return
    }

    Write-Host "Adding package '$PackageName' to the project..."
    dotnet add package $PackageName

    if ($LASTEXITCODE -eq 0) {
        Write-Host "Package '$PackageName' added successfully."
    } else {
        Write-Host "Error: Failed to add package '$PackageName'."
    }
}

# Example usage:
# Run-DotnetWithHttps
# Create-ApiController -ControllerName "Test"
# Add-DotnetPackage -PackageName "Newtonsoft.Json"
