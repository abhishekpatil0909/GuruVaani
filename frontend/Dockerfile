FROM node:18-alpine AS build
WORKDIR /app

COPY package.json package-lock.json* ./
RUN npm ci --silent

COPY . .

# Dev server
CMD ["npm", "run", "dev", "--", "--host"]
