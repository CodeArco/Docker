## For mcr.microsoft.com/dotnet/sdk:8.0
##  RUN addgroup my-app-group && adduser my-app-user --disabled-password && adduser --gecos "" my-app-user my-app-group

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS sdk

RUN addgroup my-app-group \
&& adduser my-app-user --disabled-password --gecos "" --ingroup my-app-group
USER my-app-user

WORKDIR /home/my-app-user/app/src/

COPY --chown=my-app-user ./Api.csproj .
RUN dotnet restore "Api.csproj"

COPY --chown=my-app-user . .
RUN dotnet publish "Api.csproj" --no-restore -c Release -o /home/my-app-user/app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime

RUN addgroup my-app-group \
&& adduser my-app-user --disabled-password --gecos "" --ingroup my-app-group
USER my-app-user

COPY --chown=my-app-user --from=sdk /home/my-app-user/app/publish /home/my-app-user/app/publish

WORKDIR /home/my-app-user/app/publish
CMD ["dotnet","Api.dll"]
