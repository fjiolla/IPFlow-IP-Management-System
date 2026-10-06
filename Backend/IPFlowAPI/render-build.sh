#!/bin/bash
set -e

echo "Installing .NET EF Tools..."
dotnet tool install --global dotnet-ef --version 8.0.0

echo "Restoring packages..."
dotnet restore

echo "Building application..."
dotnet build --configuration Release

echo "Running migrations..."
export PATH="$PATH:$HOME/.dotnet/tools"
dotnet ef database update

echo "Build completed successfully!"
