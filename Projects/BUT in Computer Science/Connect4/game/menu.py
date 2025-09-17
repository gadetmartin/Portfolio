import pygame

pygame.init()

WIDTH = 800
HEIGHT = 800
fps = 60
timer = pygame.time.Clock()
screen = pygame.display.set_mode([WIDTH, HEIGHT])
pygame.display.set_caption('Connect 4 !')
font = pygame.font.Font('upheavtt.ttf', 50)
color_font = pygame.font.Font('upheavtt.ttf', 50)
logo = pygame.transform.scale(pygame.image.load('Logo.png'), (825, 450))
selected_color_J1 = (255, 0, 0)  # Rouge par défaut
selected_color_J2 = (255, 255, 0)  # Jaune par défaut
pygame.mixer.music.load("music.mp3")
background_menu = pygame.image.load("main_screen.png").convert()

### Classe Button
class Button:
    def __init__(self, txt, pos, color, hover_color, text_color, hover_text_color):
        self.text = txt
        self.pos = pos
        self.button = pygame.rect.Rect((self.pos[0], self.pos[1]), (295, 60))
        self.color = color
        self.hover_color = hover_color
        self.text_color = text_color
        self.hover_text_color = hover_text_color
        self.is_clicked = False
        self.is_hovered = False
        self.on = False

    def draw(self):
        self.is_hovered = self.button.collidepoint(pygame.mouse.get_pos())
        if self.is_hovered:
            pygame.draw.rect(screen, self.hover_color, self.button, 0, 5)
            text2 = font.render(self.text, True, self.hover_text_color)
        else:
            pygame.draw.rect(screen, self.color, self.button, 0, 5)
            text2 = font.render(self.text, True, self.text_color)
        screen.blit(text2, (self.pos[0] + 15, self.pos[1] + 7))

    def check_clicked(self):
        if self.is_hovered and pygame.mouse.get_pressed()[0]:
            self.is_clicked = True
        else:
            self.is_clicked = False

        return self.is_clicked
    
### Classe ColorButton
class ColorButton:
    def __init__(self, txt, pos, color, text_color):
        self.text = txt
        self.pos = pos
        self.button = pygame.rect.Rect((self.pos[0], self.pos[1]), (210, 60))
        self.color = color
        self.text_color = text_color
        self.is_clicked = False

    def draw(self):
        pygame.draw.rect(screen, self.color, self.button, 0, 5)
        text2 = font.render(self.text, True, self.text_color)
        screen.blit(text2, (self.pos[0] + 15, self.pos[1] + 7))

    def check_clicked(self):
        if self.button.collidepoint(pygame.mouse.get_pos()) and pygame.mouse.get_pressed()[0]:
            self.is_clicked = True
        else:
            self.is_clicked = False

        return self.is_clicked
    
### Classe OnOffButton
class OnOffButton:
    colorOn = 'green'
    colorOff = 'red'
    textOn = "ON"
    textOff = "OFF"

    def __init__(self, pos):
        self.text = self.textOn
        self.pos = pos
        self.button = pygame.rect.Rect((self.pos[0], self.pos[1]), (210, 60))
        self.color = self.colorOn
        self.text_color = 'black'
        self.is_clicked = False
        self.switch = "on"

    def draw(self):
        pygame.draw.rect(screen, self.color, self.button, 0, 5)
        text2 = font.render(self.text, True, self.text_color)
        screen.blit(text2, (self.pos[0] + 15, self.pos[1] + 7))

    def get_switch(self):
        return self.switch
            
### Page de l'écran de démarrage
def draw_intro():
    menu_btn = Button('Main Menu', (260, 500), (53, 53, 53), 'yellow', 'yellow', 'black')
    menu_btn.draw()
    if menu_btn.check_clicked():
        return "menu"  # Transition vers l'écran du menu principal
    screen.blit(logo, (-10, 70))
    return "intro"  # Rester sur l'écran d'introduction

### Page du menu principal
def draw_menu():
    screen.blit(background_menu, (0, 0))
    command = -1
    txt = font.render('CONNECT 4 !', True, 'white')
    screen.blit(txt, (265, 250))

    BPlay = Button('Play', (260, 300), (53, 53, 53), 'yellow', 'yellow', 'black')
    BPlay.draw()
    BOption = Button('Options', (260, 370), (53, 53, 53), 'yellow', 'yellow', 'black')
    BOption.draw()
    BExit = Button('Exit Game', (260, 440), (53, 53, 53), 'yellow', 'yellow', 'black')
    BExit.draw()

    if BPlay.check_clicked():
        command = "game"
    if BOption.check_clicked():
        command = "option"
    if BExit.check_clicked():
        pygame.quit()
        quit()
    return command

### Page du jeu
def draw_game():
    txt = font.render('JEU', True, 'white')
    screen.blit(txt, (310, 250))
    menu_btn = Button('Main Menu', (280, 500), (53, 53, 53), 'yellow', 'yellow', 'black')
    menu_btn.draw()
    if menu_btn.check_clicked():
        return "menu"
    return "game"

### Page du menu d'options
mute_btn = OnOffButton((300, 610))
def draw_option():
    menu_btn = Button('Back', (260, 720), (53, 53, 53), 'yellow', 'yellow', 'black')
    menu_btn.draw()
    if menu_btn.check_clicked():
        return "menu"

    # Déclaration de la couleur par défaut
    global selected_color_J1
    global selected_color_J2

    # Dessine le cercle de couleur
    J1 = font.render('Player 1  color :', True, 'white')
    screen.blit(J1, (160, 40))
    color1 = color_font.render('O', True, selected_color_J1)
    screen.blit(color1, (620, 40))
    J2 = font.render('Player 2 color :', True, 'white')
    screen.blit(J2, (160, 290))
    color2 = color_font.render('O', True, selected_color_J2)
    screen.blit(color2, (620, 290))

    # Liste des boutons
    button_colorsJ1 = [
        ColorButton("Red", (80, 90), (255, 0, 0), 'black'),
        ColorButton("Green", (300, 90), (0, 255, 0), 'black'),
        ColorButton("Blue", (520, 90), (0, 0, 255), 'black'),
        ColorButton("Purple", (80, 160), (128, 0, 128), 'black'),
        ColorButton("Yellow", (300, 160), (255, 255, 0), 'black'),
        ColorButton("Orange", (520, 160), (255, 165, 0), 'black'),
        ColorButton("Pink", (300, 230), (255, 192, 203), 'black'),
    ]
    button_colorsJ2 = [
        ColorButton("Red", (80, 340), (255, 0, 0), 'black'),
        ColorButton("Green", (300, 340), (0, 255, 0), 'black'),
        ColorButton("Blue", (520, 340), (0, 0, 255), 'black'),
        ColorButton("Purple", (80, 410), (128, 0, 128), 'black'),
        ColorButton("Yellow", (300, 410), (255, 255, 0), 'black'),
        ColorButton("Orange", (520, 410), (255, 165, 0), 'black'),
        ColorButton("Pink", (300, 480), (255, 192, 203), 'black')
    ]

    # Dessine les boutons
    for button1 in button_colorsJ1:
        button1.draw()
        if button1.check_clicked():
            selected_color_J1 = button1.color

    for button2 in button_colorsJ2:
        button2.draw()
        if button2.check_clicked():
            selected_color_J2 = button2.color

    sound = font.render('Music parameter', True, 'white')
    screen.blit(sound, (180, 550))
    
    mute_btn.draw()

    return "option"

# Définir l'écran actuel comme l'écran d'introduction
current_screen = "intro"

pygame.mixer.music.play(-1)
# Boucle principale
running = True
while running:
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False
        elif event.type == pygame.MOUSEBUTTONDOWN:
            if mute_btn.button.collidepoint(pygame.mouse.get_pos()):
                if mute_btn.switch == "on":
                    mute_btn.switch = "off"
                    mute_btn.color = mute_btn.colorOff
                    mute_btn.text = mute_btn.textOff
                else:
                    mute_btn.switch = "on"
                    mute_btn.color = mute_btn.colorOn
                    mute_btn.text = mute_btn.textOn
                if mute_btn.get_switch() == "off":
                    # print(mute_btn.get_switch())
                    pygame.mixer.music.stop()
                else:
                    # print(mute_btn.get_switch())
                    pygame.mixer.music.play(-1)

    screen.fill((0, 0, 0))  # Effacer l'écran

    if current_screen == "intro":
        current_screen = draw_intro()
    elif current_screen == "menu":
        command = draw_menu()
        if command == "game":
            current_screen = "game"
        elif command == "option":
            current_screen = "option"
    elif current_screen == "game":
        current_screen = draw_game()
    elif current_screen == "option":
        current_screen = draw_option()

    pygame.display.flip()  # Mettre à jour l'affichage
    timer.tick(fps)

pygame.quit()
quit()
