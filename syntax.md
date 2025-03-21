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
     pair_alias
   | pair_policy
```

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

### Example 1: Alias

```ruby
alias exampleAlias: "example string"
```

### Example 2: Policy Rule without Categories

```ruby
policy examplePolicy: key1 = "value1"
```

### Example 3: Policy Rule with Categories

```ruby
policy (category1, category2) examplePolicyWithCategories: key2 & key3
```

### Example 4: Complex Expression

```ruby
policy complexPolicy: (key4 | key5) & !key6
```

### Example 5: Containment Operation

```ruby
policy containmentPolicy: key7 in ["value2", "value3"]
```

### Example 6: Nested Expressions

```ruby
policy nestedPolicy: (key8 = "value4") & (key9 has "value5")
```