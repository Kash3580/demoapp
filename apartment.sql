/* Stage1: */

CREATE TABLE BuildingTenants (
  building_id INT PRIMARY KEY,
  building_name VARCHAR(255),
  tenant_name VARCHAR(255),
  start_date DATE,
  end_date DATE
);

select 
     building_id,
     building_name,
     tenant_name,
     start_date
     end_date
from  BuildingTenants


/* Stage2: */

CREATE TABLE GuestLog (
  log_id INT AUTO_INCREMENT  PRIMARY KEY,
  date_entry DATETIME,
  apartment_id INT,
  num_guests INT 
);

CREATE TABLE Apartment (
  apartment_id INT AUTO_INCREMENT  PRIMARY KEY,
  building_id INT,
  building_name VARCHAR(255),
  apartment_number VARCHAR(255) ,
  num_guests int DEFAULT 0
);
 


CREATE TRIGGER update_guest_count
AFTER INSERT ON GuestLog
FOR EACH ROW
BEGIN
  IF NEW.num_guests > 0 THEN
    UPDATE Apartment
    SET num_guests = num_guests + NEW.num_guests
    WHERE apartment_id = NEW.apartment_id;
  ELSE
    UPDATE Apartment
    SET num_guests = num_guests - ABS(NEW.num_guests)
    WHERE apartment_id = NEW.apartment_id;
  END IF;
END; //
