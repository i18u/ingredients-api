FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY Ingredients.Web/*.csproj ./
RUN dotnet restore

COPY Ingredients.Web/ ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "Ingredients.Web.dll"]