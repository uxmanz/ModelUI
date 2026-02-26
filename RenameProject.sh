#!/bin/bash

# Prompt for new app name
read -p "Enter new application name: " AppName

# Validate input
if [ -z "$AppName" ]; then
  echo "Application name cannot be empty."
  exit 1
fi

# Variables
OldName="ModelUI"
NewName="$AppName"
SolutionFile="${OldName}.sln"
ProjectFolder="$OldName"
ProjectFile="${OldName}.csproj"

echo "Renaming application from $OldName to $NewName..."
echo ""

# Step 1: Replace and rename solution file
if [ -f "$SolutionFile" ]; then
  sed -i "s/$OldName/$NewName/g" "$SolutionFile"
  mv "$SolutionFile" "${NewName}.sln"
  echo "Solution file updated and renamed."
else
  echo "ERROR: Solution file not found!"
fi

# Step 2: Replace and rename .csproj
if [ -f "$ProjectFolder/$ProjectFile" ]; then
  sed -i "s/$OldName/$NewName/g" "$ProjectFolder/$ProjectFile"
  mv "$ProjectFolder/$ProjectFile" "$ProjectFolder/${NewName}.csproj"
  echo "Project file updated and renamed."
else
  echo "ERROR: Project file not found!"
fi

# Step 3: Update all .cs and .axaml files recursively
echo "Updating .cs and .axaml files..."
find "$ProjectFolder" -type f \( -name "*.cs" -o -name "*.axaml" \) -exec sed -i "s/$OldName/$NewName/g" {} \;
echo "All .cs and .axaml files updated."

# Step 4: Rename folder
if [ -d "$ProjectFolder" ]; then
  mv "$ProjectFolder" "$NewName"
  echo "Folder renamed to $NewName."
else
  echo "ERROR: Folder $ProjectFolder not found!"
fi

echo ""
echo "✅ Application renamed to $NewName successfully."
read -p "Press enter to continue..."
