# Create a table that will include the building and the tenants living there, as well as start and end dates.

CREATE TABLE BuildingTenants (
  building_id INT,
  building_name VARCHAR(255),
  tenant_name VARCHAR(255),
  start_date DATE,
  end_date DATE,
  PRIMARY KEY (building_id)
);


## Add a table of guests
### each apartment can host guests
### every time a guest enters the apartment, an entry is logged in to the table in the following form
    o date
    o apartment
    o number of guests (entering guests will be a positive, guests leaving will be negative)


### update the app to constantly show the correct number of tenants and their guests.

CREATE TABLE GuestLog (
  log_id INT AUTO_INCREMENT,
  date_logged DATE,
  apartment_id INT,
  num_guests INT,
  PRIMARY KEY (log_id)
);

CREATE TABLE GuestLog (
  log_id INT AUTO_INCREMENT,
  date_entry DATETIME,
  apartment_id INT,
  num_guests INT,
  PRIMARY KEY (log_id)
);

CREATE TABLE Apartment (
  apartment_id INT AUTO_INCREMENT,
  building_id INT,
  apartment_number VARCHAR(255),
  PRIMARY KEY (apartment_id)
);

ALTER TABLE Apartment ADD COLUMN num_tenants INT DEFAULT 0;
ALTER TABLE Apartment ADD COLUMN num_guests INT DEFAULT 0;


SELECT
  B.building_name,
  A.apartment_id,
  A.num_tenants,
  COALESCE(SUM(GL.num_guests), 0) AS num_guests
FROM
  Apartments A
LEFT JOIN
  GuestLog GL ON A.apartment_id = GL.apartment_id
JOIN
  Buildings B ON A.building_id = B.building_id
GROUP BY
  A.apartment_id;
