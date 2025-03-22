# Policy Syntax Documentation

## Syntax Structure

### Script

A script is composed of multiple pairs & ends with 'EOF`.

```ruby
script :
    pair*
    EOF
```

### Pairs

Pairs can be either aliases | policy rules.

```ruby
pair :
     pair_include
   | pair_alias
   | pair_policy
```

### Includes

An include is used for include additional file.
```ruby
pair_include : 
   'include' string
   ;

### Aliases

An alias is defined by the keyword `alias', followed by an identifier & a string.

```ruby
pair_alias : 
   'alias' alias_id ':' string
```

### Policy Rules

A policy is defined by the keyword `policy`, optionally followed by categories, an identifier, & an expression.

```ruby
pair_policy : 
   'policy' categories? policy_id ':' expression
```

### Categories
Categories are defined within parentheses & separated by commas. They allow applying filters when only certain policies need to be included.

```ruby
categories :
     '(' category (',' category)* ')'
```

### arrays
```ruby
array :
     '[' value_ref (',' value_ref)* ']'
```

### Expressions

Expressions can contain key references, boolean operations, equality operations, containment operations, & values.

```ruby
expression
   : source? key_ref ('+' | operationEqual value_ref)?
   | '!' expression
   | '(' expression ')'
   | expression operationBoolean expression
   | expression operationContains array
```

### Values

```ruby
value_ref
   : string
   | IDQUOTED
   | ID
   | boolean
   | integer
```

```ruby
source : (ID | IDQUOTED) '.';
string : STRING;
integer : INT;
alias_id : ID | IDQUOTED;
policy_id : ID ('.' ID)* | IDQUOTED;
key_ref : ID | IDQUOTED;
category : ID;
boolean : 'true' | 'false';
```

### Operations

The available operations are as follows:

```ruby
operationBoolean : `&', `|'
operationEqual : `=', `!=', `>', `<', `>=', `<='
operationContains: `in', `!in', `has', `!has'
```

### Comments


```csharp
/*
  
   multiline comment
 
 */

// on comment line

```

### Fragments

Fragments are parts of the syntax used to define more complex elements.

```ruby
fragment ESC
   : '\\' (["\\/bfnrt] | UNICODE)
```

```ruby
fragment SAFECODEPOINT
   : ~ ["\\\u0000-\u001F]
```

```ruby
fragment UNICODE
   : 'u' HEX HEX HEX HEX
```

```ruby
fragment HEX
   : [0-9a-fA-F]
```


## Examples

### Alias

```ruby
alias exampleAlias: "example string"
```

### Policy Rule without Categories

```ruby
policy examplePolicy: key1 = "value1"
```

### Policy Rule with Categories

```ruby
policy (category1, category2) examplePolicyWithCategories: key2 & key3
```

### Complex Expression

```ruby
policy complexPolicy: (key4 | key5) & !key6
```

### Containment Operation

```ruby
policy containmentPolicy: key7 in ["value2", "value3"]
```

### Nested Expressions

```ruby
policy nestedPolicy: (key8 = "value4") & (key9 has "value5")
```

### Any samples for Create policy

Create an alias for using in the policy like claim name
```batch
alias role : ""http://schemas.microsoft.com/ws/2008/06/identity/claims/role""
```

### Create a policy like a claim ope is required but the value is not important
```batch
policy p1 : ope+
```

### Create a policy like a role admin is required
```batch
policy p1 : role=admin
```

### Create a policy like a role can't be guest
```batch
policy p1 : role != guest
```

### Create a policy like a role admin is required
```batch
policy p1 : (role = admin)
```

### Create a policy like a role can't be guest
```batch
policy p1 : !(role == guest)
```

### Create a policy like a role can be admin or guest
```batch
policy p1 : role in [admin, guest]
```

### Create a policy like a role can't be admin or guest
```batch
policy p1 : role !in [admin, guest]
```

### Create a policy like a role must not to have admin and guest
```batch
policy p1 : role !has [admin, guest]
```

### Create a policy like a role must to have admin and guest
```batch
policy p1 : role has [admin, guest]
```

### Create a policy like the value to evaluate is in another object with a property name equal to "property" and value equal to 1
```batch
policy p1 : source2.property = 1
```

### Create a policy like the value to evaluate is in another object with a property name equal to "property" and value equal to 1
```batch
policy p1 : source2.property = 1
```

### Create a policy like that check if greater than 18
```batch
policy isAdult : Identity.Age >= 18
```

### Add category on a rule
```batch
policy p1 (web) : Identity.IsAuthenticated
```