# database "market_queue"

## table "area"

```
+-----------+----------------+------+-----+---------+----------------+
| Field     | Type           | Null | Key | Default | Extra          |
+-----------+----------------+------+-----+---------+----------------+
| id        | int(5)         | NO   | PRI | NULL    | auto_increment |
| latitude  | decimal(15,13) | NO   |     | NULL    |                |
| longitude | decimal(15,13) | NO   |     | NULL    |                |
+-----------+----------------+------+-----+---------+----------------+
```

## table "visitor"

````
+---------------+----------------------+------+-----+---------+----------------+
| Field         | Type                 | Null | Key | Default | Extra          |
+---------------+----------------------+------+-----+---------+----------------+
| id            | int(5)               | NO   | PRI | NULL    | auto_increment |
| area_id       | int(5)               | NO   |     | NULL    |                |
| check_in_time | datetime             | NO   |     | NULL    |                |
| duration      | smallint(5) unsigned | NO   |     | NULL    |                |
+---------------+----------------------+------+-----+---------+----------------+
```