class GroundRobot extends Phaser.Sprite {
  constructor(game, sf, x, y, image) {
    super(game, x, y, image);
    this.anchor.setTo(0.5);
    this.height = this.height/4 * sf;
    this.width = this.width/4 * sf;
    this.game.stage.addChild(this);
  }
  update() {
    //console.log('called');
  }
}

export default GroundRobot;
