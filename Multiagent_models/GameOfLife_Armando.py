# Importamos las clases que se requieren para manejar los agentes (Agent) y su entorno (Model).
# Cada modelo puede contener múltiples agentes.
from mesa import Agent, Model # Agente: Entidad. Model: Ambiente.

# Debido a que necesitamos que existe un solo agente por celda, elegimos ''SingleGrid''.
from mesa.space import SingleGrid # SingleGrid: Uno por celda

# Con ''SimultaneousActivation, hacemos que todos los agentes se activen ''al mismo tiempo''.
from mesa.time import SimultaneousActivation

# Haremos uso de ''DataCollector'' para obtener información de cada paso de la simulación.
from mesa.datacollection import DataCollector # DataCollector: Diccionario

# matplotlib lo usaremos crear una animación de cada uno de los pasos del modelo.
# %matplotlib inline
import matplotlib
import matplotlib.pyplot as plt
import matplotlib.animation as animation
plt.rcParams["animation.html"] = "jshtml"
matplotlib.rcParams['animation.embed_limit'] = 2**128

# Importamos los siguientes paquetes para el mejor manejo de valores numéricos.
import numpy as np
import pandas as pd

# Definimos otros paquetes que vamos a usar para medir el tiempo de ejecución de nuestro algoritmo.
import time
import datetime

class Cell(Agent): # Clase derivada de Agent
    def __init__(self, id, model):
        super().__init__(id, model) # Inicializamos el constructor de la clase padre
        self.live = np.random.choice([0, 1]) # Random: VIVA O MUERTA
        self.next_state = None # Siguiente estado
        
    def step(self):
        neighbors = self.model.grid.get_neighbors(self.pos, moore = True, include_center = False)  # Regresa un vector
            # Modelo de moore: las 8 celdas de alrededor
            # Modelo de von neumann: 4 celdas (arriba, abajo, izquierda, derecha)
        count = 0
        for agent in neighbors:
            count = count + agent.live # Contar los vecinos vivos

        self.next_state = self.live # Siguiente estado
        
        if self.next_state == 1: # SI estoy vivo
            if count < 2 or count > 3: # Si tengo mas de 3 o menos de 2 vecinos
                self.next_state = 0 # Muero
        else: # SI estoy muerto
            if count == 3: # SI tengo 3 vecinos
                self.next_state = 1 # Vivo

    def advance(self):
        self.live = self.next_state
                

def get_grid(model):
    grid = np.zeros( (model.grid.width, model.grid.height) )
    for (content, (x, y)) in model.grid.coord_iter():
        grid[x][y] = content.live
    return grid

class GameOfLifeModel(Model): 
    def __init__(self, width, height):
        self.grid = SingleGrid(width, height, torus = True) # Definir el espacio
        self.schedule = SimultaneousActivation(self) # Definir el modo de activacion

        for (content, (x, y)) in self.grid.coord_iter():
            agent = Cell((x, y), self) # Creamos el agente con un id y el ambiente
            self.grid.place_agent(agent, (x, y))
            self.schedule.add(agent)

        self.datacollector = DataCollector(model_reporters={
            "Grid" : get_grid})

    def step(self):
        self.datacollector.collect(self)
        self.schedule.step()



GRID_SIZE = 20
MAX_GENERATIONS = 200

model = GameOfLifeModel(GRID_SIZE, GRID_SIZE)
for i in range (MAX_GENERATIONS):
    model.step()

all_grid = model.datacollector.get_model_vars_dataframe() # Arreglo de matrices

fig, axis = plt.subplots(figsize= (5, 5))
axis.set_xticks([])
axis.set_yticks([])
patch = plt.imshow(all_grid.iloc[0][0], cmap=plt.cm.binary)


def animate(i):
    patch.set_data(all_grid.iloc[i][0])
    
anim = animation.FuncAnimation(fig, animate, frames = MAX_GENERATIONS)


plt.show()
