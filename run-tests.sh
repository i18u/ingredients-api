#!/bin/sh
dotnet test ./Ingredients.Test/Ingredients.Test.csproj --logger="junit;LogFilePath=/test-results/results.xml" /p:CollectCoverage=true /p:CoverletOutputFormat=json

echo "+--- Unit test results ---+"
cat /test-results/results.xml
echo "\n"
echo "\n"