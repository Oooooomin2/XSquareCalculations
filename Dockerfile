FROM mcr.microsoft.com/dotnet/sdk:5.0
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install --global dotnet-ef --version 5.0.11

WORKDIR /src
COPY ["XSquareCalculationsApi/XSquareCalculationsApi.csproj", "XSquareCalculationsApi/"]
RUN dotnet restore "XSquareCalculationsApi/XSquareCalculationsApi.csproj"

COPY . ./
RUN dotnet publish "XSquareCalculationsApi/XSquareCalculationsApi.csproj" -c Release -o /app/publish
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 3307
RUN chmod 744 /src/startup.sh
ENTRYPOINT ["bash","/src/startup.sh"]