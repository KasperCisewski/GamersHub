#!/usr/bin/env python
# coding: utf-8

# In[12]:


import pickle as pkl
from sklearn.neighbors import NearestNeighbors
from sklearn.decomposition import TruncatedSVD, NMF, LatentDirichletAllocation
from sklearn.feature_extraction.text import CountVectorizer, TfidfVectorizer
import requests as req
import json 
import sys


# In[13]:


with open('game_id_df.pkl', 'rb') as file:
    game_id_df = pkl.load(file)
with open('train_data_reviews.pkl', 'rb') as file2:
    train_data_reviews = pkl.load(file2)    


# In[14]:


n_comp = 20
lsa = TruncatedSVD(n_components=n_comp)


# In[15]:


tfidf_vectorizer = TfidfVectorizer(ngram_range=(1, 2),  
                                   stop_words='english', 
                                   token_pattern="\\b[a-z][a-z]+\\b",
                                   lowercase=True,
                                   max_df = 0.6)
tfidf_data = tfidf_vectorizer.fit_transform(train_data_reviews)
lsa_tfidf_data = lsa.fit_transform(tfidf_data)


# In[16]:


def get_game(names):
    #name = "The Lord of the Rings: The Battle for Middle-Earth"
    games=[]
    for name in names:
        datas= game_id_df[game_id_df.Title == name]
        if not datas.empty:
            data_index = game_id_df[game_id_df.Title == name].index[0]
            train_data_reviews[data_index]

            new_datapoint = [train_data_reviews[data_index]]
            new_vec = lsa.transform(tfidf_vectorizer.transform(new_datapoint))

            nn = NearestNeighbors(n_neighbors=5, metric='cosine', algorithm='brute')
            nn.fit(lsa_tfidf_data)

            result = nn.kneighbors(new_vec)

            
            for r in result[1][0]:
                game = game_id_df.Title[r]
                if game not in games:
                    games.append(game)
    return(games)


# In[21]:


#gamess = ['Titanfall 2', 'Battlefield: Bad Company','Battlefield 3']
#get_game(gamess)

profile = sys.argv[1]
response = req.get("https://localhost:5001/api/profile/getUserGamesNames?userId=" + profile)

data = json.loads(response.text)
games_list = []
for games in data['games']:
    games_list.append(games)
list_of_games= get_game(games_list)

with open('list_of_games.txt', 'w') as f:
    for item in list_of_games:
        f.write("%s\n" % item)

#with open('jsongames.json') as json_file:
#    data = json.load(json_file)
#games_list = []
#for games in data['games']:
#    games_list.append(games)

#list_of_games= get_game(games_list)

#with open('list_of_games.txt', 'w') as f:
#    for item in list_of_games:
#        f.write("%s\n" % item)