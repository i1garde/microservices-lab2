FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /usr/src/app
COPY package*.json ./

RUN apt-get update \
    && curl -sL https://deb.nodesource.com/setup_16.x | bash - \
    && apt-get install -y nodejs \
    && curl -sS https://dl.yarnpkg.com/debian/pubkey.gpg | apt-key add - \
    && echo "deb https://dl.yarnpkg.com/debian/ stable main" | tee /etc/apt/sources.list.d/yarn.list \
    && apt update && apt install yarn

RUN yarn install
COPY . .
RUN dotnet tool restore
EXPOSE 8080
CMD ["yarn", "start"]