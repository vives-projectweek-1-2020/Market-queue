# database "market_queue"

## table "area"

```
+-----------+----------------------+------+-----+---------+----------------+
| Field     | Type                 | Null | Key | Default | Extra          |
+-----------+----------------------+------+-----+---------+----------------+
| id        | int(5)               | NO   | PRI | NULL    | auto_increment |
| latitude  | decimal(15,13)       | NO   |     | NULL    |                |
| longitude | decimal(15,13)       | NO   |     | NULL    |                |
| visitors  | smallint(5) unsigned | YES  |     | 0       |                |
+-----------+----------------------+------+-----+---------+----------------+
```