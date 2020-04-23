# API usage

| | description|
|-----|-----|
|**/get/area**|
/ | get all area
id | get a specific area by id
latitude | get a specific area by latitude
longitude | get a specific area by longitude
latitude<br/>longitude<br/>return=area_id | get ids of the nearest areas
latitude<br/>longitude<br/>return=visitor | get the total amount of visitors in the nearest areas
|<br/>|
|**/add/area**|
latitude<br/>longitude | add an area
|<br/>|
|**/get/visitor**|
/ | get visitors which are still in an area
all=true | get all visitors (even when they aren't there anymore)
id | get a specific visitor by id which is still in an area
id<br/>all=true | get a specific visitor (even when he/she isn't there anymore)
area_id | get all visitors for a specific area when they are still there
area_id<br/>all=true | get all visitors for a specific area (even when they aren't there anymore)
area_id<br/>return=count | get the total of visitors for a specific area when they are still there
area_id<br/>all=true<br/>return=count | get the total of visitors for a specific area (even when they aren't there anymore)
|<br/>|
|**/add/visitor**|
area_id<br/>duration | add a visitor for a specific area with a duration
area_id<br/>duration<br/>offset | add a visitor for a specific area with a duration who will be there in {offset} minutes