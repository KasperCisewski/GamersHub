

# In[12]:


import pickle as pkl
from sklearn.neighbors import NearestNeighbors
from sklearn.decomposition import TruncatedSVD, NMF, LatentDirichletAllocation
from sklearn.feature_extraction.text import CountVectorizer, TfidfVectorizer
import requests as req
import json 
import sys
import pandas as pd


# In[13]:


with open('PythonScripts/game_id_df.pkl', 'rb') as file:
    game_id_df = pkl.load(file)
with open('PythonScripts/train_data_reviews.pkl', 'rb') as file2:
    train_data_reviews = pkl.load(file2)    

lsa_from_pickle = pkl.load( open( "PythonScripts/save0.p", "rb" ) )
tfidf_vectorizer_from_pickle = pkl.load( open( "PythonScripts/save1.p", "rb" ) )
tfidf_data_from_pickle = pkl.load( open( "PythonScripts/save2.p", "rb" ) )
lsa_tfidf_data_from_pickle = pkl.load( open( "PythonScripts/save3.p", "rb" ) )


# In[16]:


def get_game(names):
    games=[]
    for name in names:
        datas= game_id_df[game_id_df.Title == name]
        if not datas.empty:
            data_index = game_id_df[game_id_df.Title == name].index[0]
            train_data_reviews[data_index]

            new_datapoint = [train_data_reviews[data_index]]
            new_vec = lsa_from_pickle.transform(tfidf_vectorizer_from_pickle.transform(new_datapoint))

            nn = NearestNeighbors(n_neighbors=5, metric='cosine', algorithm='brute')
            nn.fit(lsa_tfidf_data_from_pickle)

            result = nn.kneighbors(new_vec)

            
            for r in result[1][0]:
                game = game_id_df.Title[r]
                if game not in games:
                    games.append(game)
    return(games)

profile = sys.argv[1]
response = req.get("http://192.168.0.137:5000/api/profile/games/names?userId=" + profile)

data = json.loads(response.text)
games_list = []
for games in data['games']:
    games_list.append(games)

list_of_games= get_game(games_list)

Final =pd.read_csv('delete_small.csv', sep=';')
with open('list_of_games.txt', 'w') as f:
    for item in list_of_games:
        if Final['Titile'].str.contains(item).any():
            f.write("%s\n" % item)

