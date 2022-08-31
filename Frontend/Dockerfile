### STAGE 1 ###
FROM node:lts-alpine3.13 as build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
# RUN ls
RUN npm run prod


### STAGE 2 ###
FROM nginx:stable-alpine
COPY /nginx/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/RoomanFrontend /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]