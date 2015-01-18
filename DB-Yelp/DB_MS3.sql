
#1) Given a “business category” and the current location, find the businesses 
# within 10 miles of the current location. Sort results based on
# (i) rating (ii) review count (iii) proximity to current location. 

# 70 miles per degree latitude
# 57.62 miles per degree longitude
# latitude = 33.5531 longitude = -112.1

/*
select name, stars, reviewcount, distance
from
(select name, stars, reviewcount, distLat, distLong, sqrt(distLat*distLat + distLong*distLong) as distance
from
(select name, stars, reviewcount, (latitude - 33.5531) / 0.0142857 as distLat, (longitude - -112.1) / 0.017355 as distLong
from businessTable join businessCategory on (businessTable.business_id = businessCategory.business_id)
where category = 'Chinese') subQuery1) subQuery2
where distance < 10
order by stars desc, reviewcount desc, distance;
*/

#2)
#i) For each business category, find the businesses that were rated best in June 2011. 

/*
select name, category, max(avgStars) as stars
from
(select name, category, avg(t2.stars) as avgStars, t2.reviewdate
from (businessTable join
(select * from businessCategory where category = 'Restaurants') t1
on (t1.business_id = businessTable.business_id))
join (select stars, reviewdate, business_id
from reviewTable
where reviewdate > '2011-06-01 00:00:00' and reviewdate < '2011-07-01 00:00:00') t2
on (t2.business_id = businessTable.business_id)
#order by name, t2.reviewdate
group by name) t3;
*/



#ii) Find the restaurants that steadily improved their ratings during the year of 2012. 

/*
select name
from businessTable join (select * from businessCategory where category = 'Restaurants') subQ2
on (businessTable.business_id = subQ2.business_id)
join (select * from reviewTable where reviewdate > '2012-01-01 00:00:00' and reviewdate < '2012-02-01 00:00:00') subQ3
on (subQ3.business_id = businessTable.business_id);
*/

/*
join (select * from reviewTable where reviewdate > '2012-12-01 00:00:00' and reviewdate < '2012-12-31 00:00:00') subQ4
on (subQ3.business_id = businessTable.business_id)
*/



#3) Given a business category, find the business whose reviews have the most “Traffic”.

/*
Traffic is defined as the the sum of all the stars given in all of that 
businesses’ reviews in addition to the “rating” of all the associated reviewers.
 The rating of a user is defined as the sum of their funny, useful and cool 
vote counts. The list of entries returned from this query will then be sorted 
based on their “Traffic”. The user will specify a business category then we 
will calculate the traffic for all of the business that fit this category. 
*/

/*
select name, sum(reviewTable.stars) as sumStars
from businessTable join 
(select * from businessCategory where category = 'Chinese') subQ6
on (subQ6.business_id = businessTable.business_id)
join reviewTable
on (reviewTable.business_id = businessTable.business_id)
group by businessTable.name
order by sumStars desc;
*/

/*
select name, sum(reviewTable.stars) + subQ7.userRate as traffic
from businessTable join 
(select * from businessCategory where category = 'Chinese') subQ6
on (subQ6.business_id = businessTable.business_id)
join reviewTable
on (reviewTable.business_id = businessTable.business_id)
join
(select user_id, votes_funny + votes_useful + votes_cool as userRate
from userTable) subQ7
on (reviewTable.user_id = subQ7.user_id)

group by businessTable.name
order by traffic desc;
*/


 select businessTable.name, stars, latitude, longitude
        from businessTable join (select * from businessCategory where category = 'Chinese') subQuery9
        on (businessTable.business_id = subQuery9.business_id)
	join
        (select max(stars) as maxStars, businessTable.business_id 
        from businessTable join businessCategory on (businessTable.business_id = businessCategory.business_id) 
        where category = 'Chinese') subQ5 
       
        where subQ5.maxStars = stars 
        order by stars desc 
        limit 100;    


#4) Given business category, create a density map of the highest average stars.

/*
select businessTable.name, stars, latitude, longitude
from businessTable join
(select max(stars) as maxStars, businessTable.business_id
from businessTable join businessCategory on (businessTable.business_id = businessCategory.business_id)
where category = 'Chinese') subQ5
#on (subQ5.business_id = businessTable.business_id)
where subQ5.maxStars = stars
order by stars desc
limit 100;

*/

#5) Given a User ID, find the locations of their reviewed business, and display on map.

/*select businessTable.name, latitude, longitude
from businessTable join
(select name, reviewdate, business_id
from reviewTable join userTable on (reviewTable.user_id = userTable.user_id)
where userTable.user_id = '--65q1FpAL_UQtVZ2PTGew') t4
on (t4.business_id = businessTable.business_id);
*/



