



## Install a server keycloak

### On linux
[Install a server keycloak](https://github.com/Configurations/Proxmox/blob/main/Installs/keycloak.sh)

### On docker

Create a docker-compose.yml file
```yaml

version: '3.8'

services:
  keycloak-db:
    image: postgres:15
    container_name: keycloak-db
    restart: unless-stopped
    environment:
      POSTGRES_DB: keycloak
      POSTGRES_USER: keycloak
      POSTGRES_PASSWORD: mysecretpassword
    volumes:
      - keycloak-db-data:/var/lib/postgresql/data
    networks:
      - keycloak-net

  keycloak:
    image: quay.io/keycloak/keycloak:24.0.2
    container_name: keycloak
    restart: unless-stopped
    command: start
    environment:
      KC_DB: postgres
      KC_DB_URL_HOST: keycloak-db
      KC_DB_URL_DATABASE: keycloak
      KC_DB_USERNAME: keycloak
      KC_DB_PASSWORD: mysecretpassword
      KC_HOSTNAME: localhost
      KEYCLOAK_ADMIN: admin
      KEYCLOAK_ADMIN_PASSWORD: admin
    ports:
      - "8080:8080"
    depends_on:
      - keycloak-db
    networks:
      - keycloak-net

volumes:
  keycloak-db-data:

networks:
  keycloak-net:



```

### Create a realm with a user

#### With a script
[Create a realm](https://github.com/Black-Beard-Sdk/Policy/blob/main/scripts/createrealm.sh)


```bash

curl -X POST "http://localhost:8080/realms/master/protocol/openid-connect/token" \
     -H "Content-Type: application/x-www-form-urlencoded" \
     -d "client_id=admin-cli" \
     -d "username=admin" \
     -d "password=admin" \
     -d "grant_type=password"

```

expected result is
```json
{
  "access_token": "eyJhbGciOiJIUzI1NiIsInR...",
  "expires_in": 300,
  "refresh_token": "eyJhbGciOiJIUzI1NiIsInR...",
  "refresh_expires_in": 1800,
  "token_type": "Bearer",
  "not-before-policy": 0,
  "session_state": "a123b456-c789-d012-e345-f67890abcd",
  "scope": "email profile"
}

```

curl -s -X POST "${KEYCLOAK_URL}/admin/realms" \
    -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" \
    -d '{
        "id": "'dev'",
        "realm": "'dev'",
        "enabled": true
    }'

### create a groupe for store all users.

[Create a client](https://github.com/Black-Beard-Sdk/Policy/blob/main/scripts/creategroupe.sh)


[Create a client](https://github.com/Black-Beard-Sdk/Policy/blob/main/scripts/createclient.sh)

