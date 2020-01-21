# Libraries
import matplotlib.pyplot as plt
import pandas as pd
from math import pi
import json
import requests as req
import sys

profile = sys.argv[1]

response = req.get("http://localhost:5000/api/profile/getUserGenres?userId=" + profile)
#response =("https://localhost:5001/api/profile/getUserGenres?userId=" + profile)
#print(response)

##Uncomment below function when used with req
data = json.loads(response.text)
df = pd.DataFrame({
'IdUser': data['userId'],
'Action': data['genres']['Action'],
'Shooter': data['genres']['Shooter'],
'Strategy': data['genres']['Strategy'],
'Racing': data['genres']['Racing'],
'Survival': data['genres']['Survival'],
'Adventure': data['genres']['Adventure'],
'RPG': data['genres']['RPG'],
'Simulation': data['genres']['Simulation'],
'Fighting': data['genres']['Fighting'],
'Logic': data['genres']['Logic'],
'Sports': data['genres']['Sports'],
'Arcade': data['genres']['Arcade']
}, index=[0])

##Comment Below Function when used with req
#with open('example_json.json') as json_file:
#    data = json.load(json_file)
#    df = pd.DataFrame({
#    'IdUser': data['userId'],
#    'Action': data['genres']['Action'],
#    'Shooter': data['genres']['Shooter'],
#    'Strategy': data['genres']['Strategy'],
#    'Racing': data['genres']['Racing'],
#    'Survival': data['genres']['Survival'],
 #   'Adventure': data['genres']['Adventure'],
 #   'RPG': data['genres']['RPG'],
  #  'Simulation': data['genres']['Simulation'],
   # 'Fighting': data['genres']['Fighting'],
    #'Logic': data['genres']['Logic'],
    #'Sports': data['genres']['Sports'],
    #'Arcade': data['genres']['Arcade']
    #}, index=[0])

# Set data
# number of variable
categories=list(df)[1:]
N = len(categories)


# We are going to plot the first line of the data frame.
# But we need to repeat the first value to close the circular graph:
values=df.loc[0].drop('IdUser').values.flatten().tolist()
values += values[:1]

# What will be the angle of each axis in the plot? (we divide the plot / number of variable)
angles = [n / float(N) * 2 * pi for n in range(N)]
angles += angles[:1]

# Initialise the spider plot
ax = plt.subplot(111, polar=True)

# Draw one axe per variable + add labels labels yet
plt.xticks(angles[:-1], categories, color='red', size=10)

# Draw ylabels
ax.set_rlabel_position(0)
plt.yticks([3,6,8], ["3","6","8"], color="black", size=0)
plt.ylim(0,10)

# Plot data
ax.plot(angles, values, linewidth=3, linestyle='solid',color='red')
my_palette = plt.cm.get_cmap("Set2", len(df.index))
ax.set_facecolor('purple')

# Fill area
ax.fill(angles, values, 'b', alpha=0.0)
plt.savefig('heatplot.png', transparent=False)