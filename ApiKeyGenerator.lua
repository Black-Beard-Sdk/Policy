--- ApiKeyGenerator.lua
--- Provides methods for generating API keys and security identifiers.
--- @module ApiKeyGenerator

local ApiKeyGenerator = {}

-- Characters used for password generation
local pwdchars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789()&é'(-è_çà)=$^*ù!:;,§.Nµ%£¨+°"
-- Characters used for API key generation
local chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"

--- Utility function for SHA-256 hashing.
--- @param str string The string to hash. Must not be nil.
--- @return string Binary representation of the hash.
--- @remarks
--- This implementation uses the Lua crypto library.
--- In a real environment, you should use a library like LuaCrypto
--- or an equivalent depending on your Lua environment.
--- @exception Throws an error if str is nil.
local function sha256(str)    
    local crypto = require("crypto")
    return crypto.digest("sha256", str, true)
end

--- Utility function for SHA-1 hashing.
--- @param str string The string to hash. Must not be nil.
--- @return string Binary representation of the hash.
--- @remarks
--- This implementation uses the Lua crypto library.
--- @exception Throws an error if str is nil.
local function sha1(str)
    local crypto = require("crypto")
    return crypto.digest("sha1", str, true)
end

--- Converts binary bytes to hexadecimal representation.
--- @param bytes string Binary data to convert. Must not be nil.
--- @return string Hexadecimal string representation.
--- @remarks
--- This function converts each byte to its two-character hexadecimal representation.
--- @exception Throws an error if bytes is nil.
local function toHex(bytes)
    local hex = ""
    for i = 1, #bytes do
        hex = hex .. string.format("%02x", string.byte(bytes, i))
    end
    return hex
end

--- Generates a random API key of the specified length.
--- @param length number The length of the API key. Defaults to 100 if not provided.
--- @return string A random API key of the specified length.
--- @remarks
--- This function creates a random string using alphanumeric characters.
--- It uses math.random seeded with the current time to generate random indices.
--- @exception Throws an error when length is less than or equal to 0.
--- @example
--- ```lua
--- -- Generate a 32-character API key
--- local apiKey = ApiKeyGenerator.GenerateApiKey(32)
--- print("Generated API key: " .. apiKey)
--- ```
function ApiKeyGenerator.GenerateApiKey(length)
    length = length or 100
    
    if length <= 0 then
        error("Length must be greater than 0")
    end
    
    local apiKey = {}
    local charsLength = #chars
    
    -- Utilise math.random pour générer des indices aléatoires
    math.randomseed(os.time())
    
    for i = 1, length do
        local randomIndex = math.random(1, charsLength)
        apiKey[i] = chars:sub(randomIndex, randomIndex)
    end
    
    return table.concat(apiKey)
end

--- Resolves a login by hashing raw data with SHA-256.
--- @param rawData string The raw data to hash. Must not be nil.
--- @param lengthLogin number Optional. The desired length of the login. Defaults to 25.
--- @param salt string Optional. Additional data to mix with rawData. Defaults to empty string.
--- @return string A login identifier derived from the hashed data.
--- @remarks
--- This function combines the raw data with an optional salt,
--- hashes it using SHA-256, and returns a hexadecimal representation
--- truncated to the specified length. The minimum length is 15.
--- @exception Throws an error when rawData is nil.
--- @example
--- ```lua
--- -- Generate a login from an email address
--- local email = "user@example.com"
--- local login = ApiKeyGenerator.ResolveLogin(email, 25, "mysalt")
--- print("Generated login: " .. login)
--- ```
function ApiKeyGenerator.ResolveLogin(rawData, lengthLogin, salt)
    if not rawData then
        error("rawData cannot be nil")
    end
    
    salt = salt or ""
    lengthLogin = lengthLogin or 25
    
    if lengthLogin < 15 then
        lengthLogin = 15
    end
    
    local dataToHash = rawData .. salt
    local hashedBytes = sha256(dataToHash)
    local hexString = toHex(hashedBytes)
    
    -- Tronquer si nécessaire
    if #hexString > lengthLogin then
        hexString = hexString:sub(1, lengthLogin)
    end
    
    return hexString
end

--- Generates a password by hashing raw data.
--- @param rawData string The raw data to hash. Must not be nil.
--- @param lengthPassword number Optional. The desired length of the password. Defaults to 35.
--- @param salt string The salt to mix with rawData. Must not be nil or empty.
--- @return string A password derived from the hashed data.
--- @remarks
--- This function combines the raw data with a salt, hashes it using SHA-1,
--- and converts the hash to a password using a mix of alphanumeric and special characters.
--- The minimum length is 15.
--- @exception Throws an error when rawData is nil or salt is nil or empty.
--- @example
--- ```lua
--- -- Generate a password from a login identifier
--- local login = "user123"
--- local password = ApiKeyGenerator.GeneratePassword(login, 35, "mysalt")
--- print("Generated password: " .. password)
--- ```
function ApiKeyGenerator.GeneratePassword(rawData, lengthPassword, salt)
    if not rawData then
        error("rawData cannot be nil")
    end
    
    if not salt or salt == "" then
        error("salt cannot be nil or empty")
    end
    
    lengthPassword = lengthPassword or 35
    
    if lengthPassword < 15 then
        lengthPassword = 15
    end
    
    local dataToHash = rawData .. salt
    local hashedBytes = sha1(dataToHash)
    local hexString = toHex(hashedBytes)
    
    local pwdcharsLength = #pwdchars
    local password = {}
    
    for i = 1, #hexString do
        local c = string.byte(hexString:sub(i, i))
        local index = (c % pwdcharsLength) + 1
        password[i] = pwdchars:sub(index, index)
    end
    
    local result = table.concat(password)
    
    if #result > lengthPassword then
        result = result:sub(1, lengthPassword)
    end
    
    return result
end

--- Generates a set of identifiers based on the provided API key.
--- @param apiKey string The API key to use as a base. Must not be nil.
--- @param loginLength number Optional. The desired length of the login. Defaults to 25.
--- @param pwdLength number Optional. The desired length of the password. Defaults to 35.
--- @param salt string Optional. The salt to use for login generation.
--- @return table An array containing the API key, login, and password.
--- @remarks
--- This function first generates a login by hashing the API key,
--- then generates a password by hashing the login.
--- The returned table has the following structure: {apiKey, login, password}.
--- @exception Throws errors from the called ResolveLogin and GeneratePassword functions.
--- @example
--- ```lua
--- -- Generate credentials from an email address
--- local email = "user@example.com"
--- local credentials = ApiKeyGenerator.GenerateIdentifiers(email, 25, 35, "mysalt")
--- print("API Key: " .. credentials[1])
--- print("Login: " .. credentials[2])
--- print("Password: " .. credentials[3])
--- ```
function ApiKeyGenerator.GenerateIdentifiers(apiKey, loginLength, pwdLength, salt)
    loginLength = loginLength or 25
    pwdLength = pwdLength or 35
    
    local login = ApiKeyGenerator.ResolveLogin(apiKey, loginLength, salt)
    local password = ApiKeyGenerator.GeneratePassword(login, pwdLength, apiKey)
    
    return {apiKey, login, password}
end

return ApiKeyGenerator