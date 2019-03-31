FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY ./src/Ingredients.Web/ ./Ingredients.Web/

#RUN dotnet restore ./Ingredients.Web/Ingredients.Web.csproj
RUN dotnet publish ./Ingredients.Web/Ingredients.Web.csproj -c Release -o out
         
         FROM microsoft/dotnet:aspnetcore-runtime
         WORKDIR /app
         
         COPY --from=build-env /app/Ingredients.Web/out .
         
         EXPOSE 80
         
         ENTRYPOINT ["dotnet", "Ingredients.Web.dll"]